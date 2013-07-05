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
using Titan.Resolution;

namespace Titan.Utilities
{
    public static class DeserializationUtilities
    {
        private static List<ITypeDeserializer> deserializers;
        private static Dictionary<ResolutionType, IResolutionHandler> resolutionHandlers;

        static DeserializationUtilities()
        {
            deserializers = new List<ITypeDeserializer>();
            deserializers.Add(new EnumDeserializer());
            deserializers.Add(new InterfaceDeserializer());
            deserializers.Add(new ArrayDeserializer());
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
            deserializers.Add(new KeyValuePairDeserializer());
            deserializers.Add(new DictionaryDeserializer());
            deserializers.Add(new GenericListDeserializer());
            deserializers.Add(new NonGenericListDeserializer());
            deserializers.Add(new ComplexTypeDeserializer());

            resolutionHandlers = new Dictionary<ResolutionType, IResolutionHandler>();
            resolutionHandlers.Add(ResolutionType.Property, new PropertyResolutionHandler());
            resolutionHandlers.Add(ResolutionType.CollectionItem, new CollectionItemResolutionHandler());
            resolutionHandlers.Add(ResolutionType.DictionaryKey, new DictionaryKeyResolutionHandler());
            resolutionHandlers.Add(ResolutionType.DictionaryValue, new DictionaryValueResolutionHandler());
        }

        public static object Deserialize(DeserializationRequest request)
        {
            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(request));
            return deserializer.Handle(request);
        }  

        public static IEnumerable<XObject> GetMatchingXObjects(ResolutionRequest request)
        {
            ResolutionInfo info = resolutionHandlers[request.Type].Handle(request);

            if (info.NodeType == XmlNodeType.Attribute)
            {
                return request.Root.Attributes().Where(info.Predicate);
            }
            else if (info.NodeType == XmlNodeType.Element)
            {
                return request.Root.Elements().Where(info.Predicate);
            }
            else if (info.NodeType == XmlNodeType.Text)
            {
                return new List<XObject>() { request.Root };
            }
            else if (info.NodeType == XmlNodeType.None)
            {
                return Enumerable.Empty<XObject>();
            }

            return null;
        }

        public static XObject GetMatchingXObject(ResolutionRequest request)
        {
            return GetMatchingXObjects(request).SingleOrDefault();
        }
    }
}
