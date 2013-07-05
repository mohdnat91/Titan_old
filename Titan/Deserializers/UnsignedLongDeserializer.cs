using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class UnsignedLongDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(ulong) || requset.TargetType == typeof(ulong?);
        }

        public object Handle(DeserializationRequest requset)
        {
            ulong value;
            if (ulong.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new Exception();
        }
    }
}
