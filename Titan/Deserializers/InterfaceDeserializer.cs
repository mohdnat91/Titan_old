using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.Serialization;
using Titan.Utilities;
using Titan.Utilities.Exceptions;
using Titan.Visitors;

namespace Titan.Deserializers
{
    internal class InterfaceDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject)
        {
            return type.IsInterface;
        }

        public object Handle(Type type, XObject xobject, Metadata metadata)
        {

            Type concrete = GetGivenType(metadata) ?? metadata.Conventions.GetDefaultInterfaceImplementation(type, metadata);
            if (concrete == null)
            {
                throw new DeserializationException(string.Format("No implementation was found for interface '{0}'", type));
            }
            return concrete.Accept(metadata.Visitor, metadata);
        }

        private Type GetGivenType(Metadata metadata)
        {
            XmlElementAttribute element = metadata.Attribute<XmlElementAttribute>();
            if (element != null && element.Type != null)
            {
                return element.Type;
            }

            XmlAttributeAttribute attribute = metadata.Attribute<XmlAttributeAttribute>();
            if (attribute != null && attribute.Type != null)
            {
                return attribute.Type;
            }

            return null;
        }
    }
}
