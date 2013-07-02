using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class TypeDeserializer : ITypeDeserializer
    {
        public bool CanHandle(DeserializationRequest requset)
        {
            return false;
        }

        public object Handle(DeserializationRequest requset)
        {
            throw new NotImplementedException();
        }
    }
}
