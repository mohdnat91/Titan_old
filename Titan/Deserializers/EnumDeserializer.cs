using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class EnumDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType.IsEnum;
        }

        public object Handle(DeserializationRequest requset)
        {
            return Enum.Parse(requset.TargetType, requset.Root.GetValue());
        }
    }
}
