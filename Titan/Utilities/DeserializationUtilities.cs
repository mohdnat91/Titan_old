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
using Titan.Utilities.Exceptions;

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
            deserializers.Add(new PrimitiveTypeDeserializer());
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
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            
            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(request));
            if (deserializer == null)
            {
                throw new DeserializationException("Cannot find a handler for the deserialization request");
            }

            try
            {
                return deserializer.Handle(request);
            }
            catch
            {
                throw;
            }
        }  

        public static IEnumerable<XObject> GetMatchingXObjects(ResolutionRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }
            if (!resolutionHandlers.ContainsKey(request.Type))
            {
                throw new ResolutionException(string.Format("Cannot find a handler for resolution request with type '{0}'", request.Type));
            }

            ResolutionInfo info;

            try
            {
                info = resolutionHandlers[request.Type].Handle(request);
            }
            catch
            {
                throw;
            }

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
            else
            {
                throw new ResolutionException(string.Format("Unkown node type '{0}'", info.NodeType));
            }
        }

        public static XObject GetMatchingXObject(ResolutionRequest request)
        {
            return GetMatchingXObjects(request).SingleOrDefault();
        }
    }
}
