using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Titan
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    public sealed class SectionAttribute : Attribute
    {
        public string XPath { get; set; }

        public SectionAttribute(string xpath)
        {
            XPath = xpath;
        }
    }
}
