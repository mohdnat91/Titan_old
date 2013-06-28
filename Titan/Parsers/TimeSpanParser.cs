using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Titan.Parsers
{
    public class TimeSpanParser : AbstractParser<TimeSpan>
    {
        public override TimeSpan Parse(XElement element, string xpath)
        {
            string value = xpath != null ? element.XPathSelectElements(xpath).Single().Value : element.Value;
            return TimeSpan.Parse(value);
        }

        public override bool IsValid(Type type)
        {
            return type.IsAssignableFrom(typeof(TimeSpan));
        }
    }
}
