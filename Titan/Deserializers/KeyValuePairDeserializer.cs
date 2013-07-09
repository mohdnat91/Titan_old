using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;
using Titan.Utilities.Exceptions;

namespace Titan.Deserializers
{
    internal class KeyValuePairDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.XRoot is XElement && request.TargetType.IsGenericType && request.TargetType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }

        public object Handle(DeserializationRequest request)
        {
            XElement ERoot = request.XRoot as XElement;

            Type keyType = request.TargetType.GetGenericArguments()[0];
            Type valueType = request.TargetType.GetGenericArguments()[1];

            ResolutionRequest keyRequest = new ResolutionRequest(ResolutionType.DictionaryKey, ERoot);
            keyRequest.Attributes = request.Attributes;
            keyRequest.Conventions = request.Conventions;

            XObject keyObj = XObjectMatcher.GetMatchingXObject(keyRequest);

            if (keyObj == null)
            {
                throw new NoMatchException(string.Format("Dictionary key resolution failed"));
            }

            DeserializationRequest desReq1 = new DeserializationRequest(keyObj, keyType, request.Context);

            dynamic key = request.Visitor.Deserialize(desReq1);

            ResolutionRequest valueRequest = new ResolutionRequest(ResolutionType.DictionaryValue, ERoot);
            valueRequest.Attributes = request.Attributes;
            valueRequest.Conventions = request.Conventions;

            XObject valueObj = XObjectMatcher.GetMatchingXObject(valueRequest);

            if (valueObj == null)
            {
                throw new NoMatchException(string.Format("Dictionary value resolution failed"));
            }

            DeserializationRequest desReq2 = new DeserializationRequest(valueObj, valueType, request.Context);

            dynamic value = request.Visitor.Deserialize(desReq2);

            dynamic kvp = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType).GetConstructor(new[] { keyType, valueType }).Invoke(new[] { key, value });

            return kvp;
        }
    }
}
