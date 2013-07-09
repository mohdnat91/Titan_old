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
    internal class DictionaryDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.XRoot is XElement && request.TargetType.IsAssignableToGenericType(typeof(IDictionary<,>));
        }

        public object Handle(DeserializationRequest request)
        {
            dynamic dictionary = Activator.CreateInstance(request.TargetType);
            XElement ERoot = request.XRoot as XElement;

            Type[] types = request.TargetType.GetParentTypeParameters(typeof(IDictionary<,>));
            Type kvp = typeof(KeyValuePair<,>).MakeGenericType(types);

            IEnumerable<XElement> children = ERoot.Elements();

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest(child, kvp, request.Context);
                childReq.Attributes = request.Attributes;

                dynamic value = request.Visitor.Deserialize(childReq);
                dictionary[value.Key] = value.Value;
            }

            return dictionary;
        }
    }
}
