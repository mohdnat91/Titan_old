using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class InterfaceDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.TargetType.IsInterface;
        }

        public object Handle(DeserializationRequest request)
        {
            Type concrete = request.Conventions.GetDefaultInterfaceImplementation(request);
            request.TargetType = concrete;
            return DeserializationUtilities.Deserialize(request);
        }
    }
}
