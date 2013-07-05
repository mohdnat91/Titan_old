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
            return request.Root is XElement && request.TargetType.IsGenericType && request.TargetType.GetGenericTypeDefinition() == typeof(KeyValuePair<,>);
        }

        public object Handle(DeserializationRequest request)
        {
            XElement ERoot = request.Root as XElement;

            Type keyType = request.TargetType.GetGenericArguments()[0];
            Type valueType = request.TargetType.GetGenericArguments()[1];

            ResolutionRequest keyRequest = new ResolutionRequest();
            keyRequest.Attributes = request.Attributes;
            keyRequest.Conventions = request.Conventions;
            keyRequest.Root = ERoot;
            keyRequest.Type = ResolutionType.DictionaryKey;

            XObject keyObj = DeserializationUtilities.GetMatchingXObject(keyRequest);

            if (keyObj == null)
            {
                throw new NoMatchException(string.Format("Dictionary key resolution failed"));
            }

            DeserializationRequest desReq1 = new DeserializationRequest();
            desReq1.TargetType = keyType;
            desReq1.Root = keyObj;
            desReq1.Conventions = request.Conventions;
            desReq1.Attributes = request.Attributes;

            dynamic key = DeserializationUtilities.Deserialize(desReq1);

            ResolutionRequest valueRequest = new ResolutionRequest();
            valueRequest.Attributes = request.Attributes;
            valueRequest.Conventions = request.Conventions;
            valueRequest.Root = ERoot;
            valueRequest.Type = ResolutionType.DictionaryValue;

            XObject valueObj = DeserializationUtilities.GetMatchingXObject(valueRequest);

            if (valueObj == null)
            {
                throw new NoMatchException(string.Format("Dictionary value resolution failed"));
            }

            DeserializationRequest desReq2 = new DeserializationRequest();
            desReq2.TargetType = valueType;
            desReq2.Root = valueObj;
            desReq2.Conventions = request.Conventions;
            desReq2.Attributes = request.Attributes;

            dynamic value = DeserializationUtilities.Deserialize(desReq2);

            dynamic kvp = typeof(KeyValuePair<,>).MakeGenericType(keyType, valueType).GetConstructor(new[] { keyType, valueType }).Invoke(new[] { key, value });

            return kvp;
        }
    }
}
