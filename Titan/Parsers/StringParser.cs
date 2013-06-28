using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Titan.Parsers
{
    public class StringParser : AbstractParser<string>
    {
        public override string Parse(XElement element, string xpath)
        {
            return xpath != null ? element.XPathSelectElements(xpath).Single().Value : element.Value;
        }

        public override bool IsValid(Type type)
        {
            return type.IsAssignableFrom(typeof(string));
        }
    }
}
