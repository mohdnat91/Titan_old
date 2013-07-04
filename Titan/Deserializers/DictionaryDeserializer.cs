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
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.Root is XElement && requset.TargetType.IsAssignableToGenericType(typeof(IDictionary<,>));
        }

        public object Handle(DeserializationRequest requset)
        {
            dynamic dictionary = Activator.CreateInstance(requset.TargetType);
            XElement ERoot = requset.Root as XElement;

            Type keyType = requset.TargetType.GetGenericArguments()[0];
            Type valueType = requset.TargetType.GetGenericArguments()[1];
            Type kvp = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType);

            IEnumerable<XElement> children = ERoot.Elements();

            foreach (XElement child in children)
            {
                DeserializationRequest childReq = new DeserializationRequest();
                childReq.Conventions = requset.Conventions;
                childReq.Root = child;
                childReq.Attributes = requset.Attributes;
                childReq.TargetType = kvp;
                dynamic value = DeserializationUtilities.Deserialize(childReq);
                dictionary[value.Key] = value.Value;
            }

            return dictionary;
        }
    }
}
