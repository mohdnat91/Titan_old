using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Titan.Utilities;
using Titan.Utilities.Exceptions;

namespace Titan.Deserializers
{
    internal class ComplexTypeDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.XRoot is XElement;
        }

        public object Handle(DeserializationRequest request)
        {
            object target = Activator.CreateInstance(request.TargetType);

            IEnumerable<PropertyInfo> properties = request.TargetType.GetProperties().Where(p => p.CanWrite);

            foreach (PropertyInfo property in properties)
            {
                ResolutionRequest resolve = new ResolutionRequest(ResolutionType.Property, request.XRoot as XElement);
                resolve.Attributes = property.GetCustomAttributes<Attribute>();
                resolve.Context["property"] = property;
                resolve.Conventions = request.Conventions;

                XObject matching = XObjectMatcher.GetMatchingXObject(resolve);
                if (matching == null)
                {
                    if (property.PropertyType.IsNullable())
                    {
                        property.SetValue(target, null);
                        continue;
                    }
                    else
                    {
                        throw new NoMatchException(string.Format("Did not find a match for '{0}' property", property.Name));
                    }
                }

                DeserializationRequest propReq = new DeserializationRequest(matching, property.PropertyType, request.Context) { Attributes = property.GetCustomAttributes<Attribute>() };

                object value = request.Visitor.Deserialize(propReq);
                property.SetValue(target, value);
            }

            return target;
        }
    }
}
