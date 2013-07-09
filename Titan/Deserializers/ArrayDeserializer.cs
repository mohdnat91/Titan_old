using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Attributes;
using Titan.Utilities;
using Titan.Visitors;

namespace Titan.Deserializers
{
    internal class ArrayDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject) {
            return xobject is XElement && type.IsArray;
        }

        public object Handle(Type type, XObject xobject, Metadata metadata) {
            Type childType = type.GetElementType();

            ResolutionRequest childResolution = new ResolutionRequest(ResolutionType.CollectionItem, xobject as XElement, metadata);

            IEnumerable<XObject> children = XObjectMatcher.GetMatchingXObjects(childResolution);

            dynamic collection = Array.CreateInstance(childType, children.Count());

            int i = 0;

            foreach (XElement child in children) {
                Metadata childMeta = new Metadata(metadata);
                childMeta.Set("xobject", child);

                dynamic value = childType.Accept(metadata.Visitor, childMeta);
                collection[i++] = value;
            }

            return collection;
        }
    }
}
