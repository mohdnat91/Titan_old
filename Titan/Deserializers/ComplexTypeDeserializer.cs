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
            return request.Root is XElement;
        }

        public object Handle(DeserializationRequest request)
        {
            object target = Activator.CreateInstance(request.TargetType);

            IEnumerable<PropertyInfo> properties = request.TargetType.GetProperties().Where(p => p.CanWrite);

            foreach (PropertyInfo property in properties)
            {
                ResolutionRequest resolve = new ResolutionRequest();
                resolve.Root = request.Root as XElement;
                resolve.Attributes = property.GetCustomAttributes<Attribute>();
                resolve.Type = ResolutionType.Property;
                resolve.Context["property"] = property;
                resolve.Conventions = request.Conventions;

                XObject matching = DeserializationUtilities.GetMatchingXObject(resolve);
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

                DeserializationRequest propReq = new DeserializationRequest() { TargetType = property.PropertyType, Root = matching, Attributes = property.GetCustomAttributes<Attribute>() };
                propReq.Conventions = request.Conventions;
                object value = DeserializationUtilities.Deserialize(propReq);
                property.SetValue(target, value);
            }

            return target;
        }
    }
}
