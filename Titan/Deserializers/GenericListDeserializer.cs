using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;
using Titan.Visitors;

namespace Titan.Deserializers
{
    internal class GenericListDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject)
        {
            return xobject is XElement && type.IsAssignableToGenericType(typeof(IList<>));
        }

        public object Handle(Type type, XObject xobject, Metadata metadata)
        {
            dynamic collection = Activator.CreateInstance(type);

            ResolutionRequest childResolution = new ResolutionRequest(ResolutionType.CollectionItem, xobject as XElement, metadata);

            IEnumerable<XObject> children = XObjectMatcher.GetMatchingXObjects(childResolution);

            Type childType = type.GetParentTypeParameters(typeof(IList<>)).Single();

            foreach (XElement child in children)
            {
                Metadata childMetadata = new Metadata(metadata);
                childMetadata.Set("xobject", child);

                dynamic value = childType.Accept(metadata.Visitor, childMetadata);
               
                collection.Add(value);
            }

            return collection;
        }
    }
}
