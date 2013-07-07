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
            return request.Root is XElement && request.TargetType.IsArray;
        }


        public object Handle(DeserializationRequest request)
        {
            XElement ERoot = request.Root as XElement;

            Type childType = request.TargetType.GetElementType();

            ResolutionRequest childResolution = new ResolutionRequest();
            childResolution.Root = ERoot;
            childResolution.Attributes = request.Attributes;
            childResolution.Type = ResolutionType.CollectionItem;
            childResolution.Conventions = request.Conventions;

            IEnumerable<XObject> children = DeserializationUtilities.GetMatchingXObjects(childResolution);

            dynamic collection = Array.CreateInstance(childType, children.Count());

            int i = 0;

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest() { TargetType = childType, Root = child };
                childReq.Conventions = request.Conventions;
                childReq.Visitor = request.Visitor;
                dynamic value = request.Visitor.Deserialize(childReq);
                collection[i++] = value;
            }

            return collection;
        }
    }
}
