using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class DoubleDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return requset.TargetType == typeof(double) || requset.TargetType == typeof(double?);
        }

        public object Handle(DeserializationRequest requset)
        {
            double value;
            if (double.TryParse(requset.Root.GetValue(), out value)) return value;
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
