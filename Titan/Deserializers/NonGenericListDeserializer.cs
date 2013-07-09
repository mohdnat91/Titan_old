using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Attributes;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class NonGenericListDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.XRoot is XElement && typeof(IList).IsAssignableFrom(request.TargetType);
        }


        public object Handle(DeserializationRequest request)
        {
            IList collection = (IList)Activator.CreateInstance(request.TargetType);

            ResolutionRequest childResolution = new ResolutionRequest(ResolutionType.CollectionItem, (XElement)request.XRoot);
            childResolution.Attributes = request.Attributes;
            childResolution.Conventions = request.Conventions;

            IEnumerable<XObject> children = XObjectMatcher.GetMatchingXObjects(childResolution);

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest(child, typeof(string), request.Context);
                object value = request.Visitor.Deserialize(childReq);
                collection.Add(value);
            }

            return collection;
        }
    }
}
