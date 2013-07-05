using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class IntegerDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(int) || requset.TargetType == typeof(int?);
        }

        public object Handle(DeserializationRequest requset)
        {
            int value;
            if (int.TryParse(requset.Root.GetValue(), out value)) return value;
            throw new Exception();
        }
    }
}
