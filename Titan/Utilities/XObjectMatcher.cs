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
    public static class XObjectMatcher
    {
        private static Dictionary<ResolutionType, IResolutionHandler> resolutionHandlers;

        static XObjectMatcher()
        {
            resolutionHandlers = new Dictionary<ResolutionType, IResolutionHandler>();
            resolutionHandlers.Add(ResolutionType.Property, new PropertyResolutionHandler());
            resolutionHandlers.Add(ResolutionType.CollectionItem, new CollectionItemResolutionHandler());
            resolutionHandlers.Add(ResolutionType.DictionaryKey, new DictionaryKeyResolutionHandler());
            resolutionHandlers.Add(ResolutionType.DictionaryValue, new DictionaryValueResolutionHandler());
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
