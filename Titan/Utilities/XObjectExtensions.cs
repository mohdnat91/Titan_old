using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace Titan.Utilities
{
    public static class XObjectExtensions
    {
        public static string GetValue(this XObject node)
        {
            switch (node.NodeType)
            {
                case XmlNodeType.Attribute:
                    return ((XAttribute)node).Value;
                case XmlNodeType.Element:
                    return ((XElement)node).Value;
                default:
                    return null;
            }
        }
    }
}
