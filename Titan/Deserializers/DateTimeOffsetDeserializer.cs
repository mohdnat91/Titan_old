using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class DateTimeOffsetDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(DateTimeOffset) || requset.TargetType == typeof(DateTimeOffset?);
        }

        public object Handle(DeserializationRequest requset)
        {
            DateTimeOffset value;
            if (DateTimeOffset.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new Exception();
        }
    }
}
