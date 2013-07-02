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
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType.IsInterface;
        }

        public object Handle(DeserializationRequest requset)
        {
            Type concrete = Conventions.GetDefaultImplementation(requset.TargetType);
            requset.TargetType = concrete;
            return DeserializationUtilities.Deserialize(requset);
        }
    }
}
