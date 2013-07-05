using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Titan.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    public sealed class XmlDictionaryEntryAttribute : Attribute
    {
        public string KeyName { get; set; }
        public string ValueName { get; set; }
        public XmlNodeType KeyNodeType { get; set; }
        public XmlNodeType ValueNodeType { get; set; }

        public int Level { get; set; }
    }

}
