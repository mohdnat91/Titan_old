using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Titan.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class XmlDictionaryValueAttribute : Attribute
    {
        public string Name { get; set; }
        public XmlNodeType NodeType { get; set; }
        public int Level { get; set; }

        public XmlDictionaryValueAttribute()
        {
            Name = null;
            NodeType = XmlNodeType.None;
            Level = 0;
        }
    }
}
