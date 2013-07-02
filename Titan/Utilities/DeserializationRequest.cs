using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titan.Utilities
{
    public class DeserializationRequest : AbstractRequest
    {
        public Type TargetType { get; set; }
    }
}
