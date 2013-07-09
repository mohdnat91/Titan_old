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
    internal class DictionaryDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject)
        {
            return xobject is XElement && type.IsAssignableToGenericType(typeof(IDictionary<,>));
        }

        public object Handle(Type type, XObject xobject, Metadata metadata)
        {
            dynamic dictionary = Activator.CreateInstance(type);
            XElement ERoot = xobject as XElement;

            Type[] types = type.GetParentTypeParameters(typeof(IDictionary<,>));
            Type kvp = typeof(KeyValuePair<,>).MakeGenericType(types);

            IEnumerable<XElement> children = ERoot.Elements();

            foreach (XElement child in children)
            {
                Metadata childMeta = new Metadata(metadata);
                childMeta.Set("xobject", child);

                dynamic value = kvp.Accept(metadata.Visitor, childMeta);
                dictionary[value.Key] = value.Value;
            }

            return dictionary;
        }
    }
}
