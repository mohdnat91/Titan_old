using System;
using System.Collections;
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
    internal class NonGenericListDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject)
        {
            return xobject is XElement && typeof(IList).IsAssignableFrom(type);
        }


        public object Handle(Type type, XObject xobject, Metadata metadata)
        {
            IList collection = (IList)Activator.CreateInstance(type);

            ResolutionRequest childResolution = new ResolutionRequest(ResolutionType.CollectionItem, xobject as XElement, metadata);

            IEnumerable<XObject> children = XObjectMatcher.GetMatchingXObjects(childResolution);

            foreach (XElement child in children)
            {
                Metadata childMeta = new Metadata(metadata);
                childMeta.Set("xobject", child);

                object value = typeof(string).Accept(metadata.Visitor, childMeta);

                collection.Add(value);
            }

            return collection;
        }
    }
}
