using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Titan.Utilities
{
    public class ResolutionInfo
    {
        public string Name { get; set; }
        public XmlNodeType NodeType { get; set; }

        public ResolutionInfo()
        {
            Name = null;
            NodeType = XmlNodeType.None;
        }
    }
}
