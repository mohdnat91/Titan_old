using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class DecimalDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(decimal) || requset.TargetType == typeof(decimal?);
        }

        public object Handle(DeserializationRequest requset)
        {
            decimal value;
            if (decimal.TryParse(requset.Root.GetValue(), out value)) return value;
            if (requset.TargetType.IsNullable())
            {
                return null;
            }
            else
            {
                throw new InvalidCastException();
            }
        }
    }
}
