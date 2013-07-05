using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class LongDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(long) || requset.TargetType == typeof(long?);
        }

        public object Handle(DeserializationRequest requset)
        {
            long value;
            if (long.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new Exception();
        }
    }
}
