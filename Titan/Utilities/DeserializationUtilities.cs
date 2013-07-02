using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Titan.Attributes;
using Titan.Deserializers;

namespace Titan.Utilities
{
    public static class DeserializationUtilities
    {
        private static List<ITypeDeserializer> deserializers;
        private static List<IAttributeHandler> attributeHandlers;

        static DeserializationUtilities()
        {
            deserializers = new List<ITypeDeserializer>();
            deserializers.Add(new EnumDeserializer());
            deserializers.Add(new InterfaceDeserializer());
            deserializers.Add(new IntegerDeserializer());
            deserializers.Add(new LongDeserializer());
            deserializers.Add(new UnsignedIntegerDeserializer());
            deserializers.Add(new UnsignedLongDeserializer());
            deserializers.Add(new DateTimeDeserializer());
            deserializers.Add(new DateTimeOffsetDeserializer());
            deserializers.Add(new TimeSpanDeserializer());
            deserializers.Add(new BooleanDesrializer());
            deserializers.Add(new StringDeserializer());
            deserializers.Add(new GuidDeserializer());
            deserializers.Add(new FloatDeserializer());
            deserializers.Add(new DoubleDeserializer());
            deserializers.Add(new DecimalDeserializer());
            deserializers.Add(new FileDeserializer());
            deserializers.Add(new DirectoryDeserializer());
            deserializers.Add(new GenericListDeserializer());
            deserializers.Add(new NonGenericListDeserializer());
            deserializers.Add(new ComplexTypeDeserializer());

            attributeHandlers = new List<IAttributeHandler>();
            attributeHandlers.Add(new XmlElementMappingAttributeHandler());
            attributeHandlers.Add(new XmlAttributeMappingAttributeHandler());
        }

        public static object Deserialize(DeserializationRequest request)
        {
            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(request));
            return deserializer.Handle(request);
        }  

        public static IEnumerable<XObject> GetMatchingXObjects(ResolutionRequest request)
        {
            ResolutionInfo info = new ResolutionInfo();

            attributeHandlers.Where(h => h.CanHandle(request)).ToList().ForEach(h => h.Handle(request, info));

            string name = GetTagName(request, info);

            if (info.NodeType == XmlNodeType.None)
            {
                info.NodeType = Conventions.GetDefaultNodeType(request);
            }

            if (info.NodeType == XmlNodeType.Attribute)
            {
                return request.Root.Attributes(name);
            }
            else if (info.NodeType == XmlNodeType.Element)
            {
                return request.Root.Elements(name);
            }

            return null;
        }

        private static string GetTagName(ResolutionRequest request, ResolutionInfo info)
        {
            if (info.Name != null)
            {
                return info.Name;
            }
            else
            {
                return Conventions.GetDefaultTagName(request);
            }
        }

        public static XObject GetMatchingXObject(ResolutionRequest request)
        {
            return GetMatchingXObjects(request).Single();
        }
    }
}
