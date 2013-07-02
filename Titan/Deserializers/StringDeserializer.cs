using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class StringDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest request)
        {
            return request.TargetType == typeof(string);
        }

        public object Handle(DeserializationRequest request)
        {
            return request.Root.GetValue();
        }
    }
}
