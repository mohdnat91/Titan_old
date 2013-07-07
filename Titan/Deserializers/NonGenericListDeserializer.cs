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
            return request.Root is XElement && typeof(IList).IsAssignableFrom(request.TargetType);
        }


        public object Handle(DeserializationRequest request)
        {
            IList collection = (IList)Activator.CreateInstance(request.TargetType);

            XElement ERoot = request.Root as XElement;

            ResolutionRequest childResolution = new ResolutionRequest();
            childResolution.Root = ERoot;
            childResolution.Attributes = request.Attributes;
            childResolution.Type = ResolutionType.CollectionItem;
            childResolution.Conventions = request.Conventions;

            IEnumerable<XObject> children = DeserializationUtilities.GetMatchingXObjects(childResolution);

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest() { TargetType = typeof(string), Root = child };
                childReq.Conventions = request.Conventions;
                childReq.Visitor = request.Visitor;
                object value = request.Visitor.Deserialize(childReq);
                collection.Add(value);
            }

            return collection;
        }
    }
}
