using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class GenericListDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.XRoot is XElement && request.TargetType.IsAssignableToGenericType(typeof(IList<>));
        }

        public object Handle(DeserializationRequest request)
        {
            dynamic collection = Activator.CreateInstance(request.TargetType);

            ResolutionRequest childResolution = new ResolutionRequest(ResolutionType.CollectionItem, request.XRoot as XElement);
            childResolution.Attributes = request.Attributes;
            childResolution.Conventions = request.Conventions;

            IEnumerable<XObject> children = XObjectMatcher.GetMatchingXObjects(childResolution);

            Type childType = request.TargetType.GetParentTypeParameters(typeof(IList<>))[0];

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest(child, childType, request.Context);
                dynamic value = request.Visitor.Deserialize(childReq);
               
                collection.Add(value);
            }

            return collection;
        }
    }
}
