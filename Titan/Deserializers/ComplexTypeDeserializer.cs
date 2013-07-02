using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

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

                XObject matching = DeserializationUtilities.GetMatchingXObject(resolve);

                DeserializationRequest propReq = new DeserializationRequest() { TargetType = property.PropertyType, Root = matching, Attributes = property.GetCustomAttributes<Attribute>() };
                object value = DeserializationUtilities.Deserialize(propReq);
                property.SetValue(target, value);
            }

            return target;
        }
    }
}
