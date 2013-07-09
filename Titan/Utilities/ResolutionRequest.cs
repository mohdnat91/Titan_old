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
        public new XElement XRoot { get { return base.XRoot as XElement; } set { base.XRoot = value; } }
        public ResolutionType Type { get; set; }

        public ResolutionRequest(ResolutionType type, XElement xroot)
        {
            XRoot = xroot;
            Type = type;
        }

        public ResolutionRequest(ResolutionType type, XElement xroot, Dictionary<string,object> context) : base(context)
        {
            XRoot = xroot;
            Type = type;
        }
    }
}
