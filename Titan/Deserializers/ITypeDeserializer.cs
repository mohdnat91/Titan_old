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
        bool CanHandle(Type type, XObject xobject);
        object Handle(Type type, XObject xobject, Metadata metadata);
    }
}
