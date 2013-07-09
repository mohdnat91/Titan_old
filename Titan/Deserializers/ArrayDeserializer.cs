using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Attributes;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class ArrayDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.XRoot is XElement && request.TargetType.IsArray;
        }


        public object Handle(DeserializationRequest request)
        {
            Type childType = request.TargetType.GetElementType();

            ResolutionRequest childResolution = new ResolutionRequest(ResolutionType.CollectionItem, request.XRoot as XElement);
            childResolution.Attributes = request.Attributes;
            childResolution.Conventions = request.Conventions;

            IEnumerable<XObject> children = XObjectMatcher.GetMatchingXObjects(childResolution);

            dynamic collection = Array.CreateInstance(childType, children.Count());

            int i = 0;

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest(child, childType, request.Context);
                dynamic value = request.Visitor.Deserialize(childReq);
                collection[i++] = value;
            }

            return collection;
        }
    }
}
