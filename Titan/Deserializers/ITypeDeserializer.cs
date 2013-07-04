using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Deserializers
{
    public interface ITypeDeserializer
    {
        bool CanHandle(DeserializationRequest request);

        object Handle(DeserializationRequest request);
    }
}
