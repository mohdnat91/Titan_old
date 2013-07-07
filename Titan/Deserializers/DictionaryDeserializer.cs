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
            return request.Root is XElement && request.TargetType.IsAssignableToGenericType(typeof(IDictionary<,>));
        }

        public object Handle(DeserializationRequest request)
        {
            dynamic dictionary = Activator.CreateInstance(request.TargetType);
            XElement ERoot = request.Root as XElement;

            Type[] types = request.TargetType.GetParentTypeParameters(typeof(IDictionary<,>));
            Type kvp = typeof(KeyValuePair<,>).MakeGenericType(types);

            IEnumerable<XElement> children = ERoot.Elements();

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest();
                childReq.Conventions = request.Conventions;
                childReq.Root = child;
                childReq.Attributes = request.Attributes;
                childReq.TargetType = kvp;
                childReq.Visitor = request.Visitor;
                dynamic value = request.Visitor.Deserialize(childReq);
                dictionary[value.Key] = value.Value;
            }

            return dictionary;
        }
    }
}
