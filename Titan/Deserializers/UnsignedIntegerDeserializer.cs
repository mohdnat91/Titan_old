using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class UnsignedIntegerDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(uint) || requset.TargetType == typeof(uint?);
        }

        public object Handle(DeserializationRequest requset)
        {
            uint value;
            if (uint.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new Exception();
        }
    }
}
