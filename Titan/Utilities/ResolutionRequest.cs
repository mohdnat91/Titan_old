using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titan.Utilities
{
    public class ResolutionRequest : AbstractRequest
    {
        public new XElement Root { get { return base.Root as XElement; } set { base.Root = value; } }
        public ResolutionType Type { get; set; }
    }
}
