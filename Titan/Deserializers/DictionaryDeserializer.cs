using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Deserializers
{
    internal class DictionaryDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Utilities.DeserializationRequest requset)
        {
            return false;
        }

        public object Handle(Utilities.DeserializationRequest requset)
        {
            throw new NotImplementedException();
        }
    }
}
