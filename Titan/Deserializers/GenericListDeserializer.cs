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
            return request.Root is XElement && request.TargetType.IsAssignableToGenericType(typeof(IList<>));
        }

        public object Handle(DeserializationRequest request)
        {
            dynamic collection = Activator.CreateInstance(request.TargetType);

            XElement ERoot = request.Root as XElement;

            ResolutionRequest childResolution = new ResolutionRequest();
            childResolution.Root = ERoot;
            childResolution.Attributes = request.Attributes;
            childResolution.Type = ResolutionType.CollectionItem;
            childResolution.Conventions = request.Conventions;

            IEnumerable<XObject> children = DeserializationUtilities.GetMatchingXObjects(childResolution);

            Type childType = request.TargetType.GetParentTypeParameter(typeof(IList<>));

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest() { TargetType = childType, Root = child };
                childReq.Conventions = request.Conventions;
                dynamic value = DeserializationUtilities.Deserialize(childReq);
               
                collection.Add(value);
            }

            return collection;
        }
    }
}
