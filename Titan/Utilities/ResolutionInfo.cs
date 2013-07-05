using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Titan.Utilities
{
    public class ResolutionInfo
    {
        public Func<XObject, bool> Predicate { get; set; }
        public XmlNodeType NodeType { get; set; }

        public ResolutionInfo()
        {
            Predicate = (x => false);
            NodeType = XmlNodeType.None;
        }
    }
}
