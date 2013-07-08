using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Titan.Utilities;
using Titan.Utilities.Exceptions;

namespace Titan.Deserializers
{
    internal class InterfaceDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.TargetType.IsInterface;
        }

        public object Handle(DeserializationRequest request)
        {

            Type concrete = GetGivenType(request) ?? request.Conventions.GetDefaultInterfaceImplementation(request);
            if (concrete == null)
            {
                throw new DeserializationException(string.Format("No implementation was found for interface '{0}'", request.TargetType));
            }
            request.TargetType = concrete;
            return request.Visitor.Deserialize(request);
        }

        private Type GetGivenType(DeserializationRequest request)
        {
            XmlElementAttribute element = request.GetAttribute<XmlElementAttribute>();
            if (element != null && element.Type != null)
            {
                return element.Type;
            }

            XmlAttributeAttribute attribute = request.GetAttribute<XmlAttributeAttribute>();
            if (attribute != null && attribute.Type != null)
            {
                return attribute.Type;
            }

            return null;
        }
    }
}
