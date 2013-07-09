using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class EnumDeserializer : ITypeDeserializer
    {
        public bool CanHandle(Type type, XObject xobject)
        {
            return type.IsEnum;
        }

        public object Handle(Type type, XObject xobject, Metadata metadata)
        {
            return Enum.Parse(type, xobject.GetValue());
        }
    }
}
