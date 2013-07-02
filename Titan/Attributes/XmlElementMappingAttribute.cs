using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    public sealed class XmlElementMappingAttribute : Attribute
    {
        public string TagName { get; set; }

        public XmlElementMappingAttribute(string tagName)
        {
            TagName = tagName;
        }
    }
}
