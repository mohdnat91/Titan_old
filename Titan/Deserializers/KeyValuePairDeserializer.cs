using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;
using Titan.Utilities.Exceptions;
using Titan.Visitors;

namespace Titan.Deserializers
{
    internal class KeyValuePairDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject)
        {
            return xobject is XElement && type.IsGenericType && type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }

        public object Handle(Type type, XObject xobject, Metadata metadata)
        {
            XElement ERoot = xobject as XElement;

            Type keyType = type.GetGenericArguments()[0];
            Type valueType = type.GetGenericArguments()[1];

            ResolutionRequest keyRequest = new ResolutionRequest(ResolutionType.DictionaryKey, ERoot, metadata);

            XObject keyObj = XObjectMatcher.GetMatchingXObject(keyRequest);

            if (keyObj == null)
            {
                throw new NoMatchException(string.Format("Dictionary key resolution failed"));
            }

            Metadata keyMeta = new Metadata(metadata);
            keyMeta.Set("xobject", keyObj);

            dynamic key = keyType.Accept(metadata.Visitor, keyMeta);

            ResolutionRequest valueRequest = new ResolutionRequest(ResolutionType.DictionaryValue, ERoot, metadata);

            XObject valueObj = XObjectMatcher.GetMatchingXObject(valueRequest);

            if (valueObj == null)
            {
                throw new NoMatchException(string.Format("Dictionary value resolution failed"));
            }

            Metadata valueMeta = new Metadata(metadata);
            valueMeta.Set("xobject",valueObj);

            dynamic value = valueType.Accept(metadata.Visitor, valueMeta);

            dynamic kvp = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType).GetConstructor(new[] { keyType, valueType }).Invoke(new[] { key, value });

            return kvp;
        }
    }
}
