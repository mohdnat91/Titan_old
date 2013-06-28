using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Titan.Parsers
{
    public class BooleanParser : AbstractParser<bool>
    {
        public override bool Parse(XElement element, string xpath)
        {
            string value = xpath != null ? element.XPathSelectElements(xpath).Single().Value : element.Value;
            return bool.Parse(value);
        }

        public override bool IsValid(Type type)
        {
            return type.IsAssignableFrom(typeof(bool));
        }
    }
}
