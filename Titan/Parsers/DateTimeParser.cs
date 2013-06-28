using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;

namespace Titan.Parsers
{
    public class DateTimeParser : AbstractParser<DateTime>
    {
        DateTimeFormatInfo formatter;

        public DateTimeParser()
        {
            formatter = new DateTimeFormatInfo();
            formatter.DateSeparator = "-";
        }

        public override DateTime Parse(XElement element, string xpath)
        {
            string value = xpath != null ? element.XPathSelectElements(xpath).Single().Value : element.Value;
            return DateTime.Parse(value, formatter);
        }

        public override bool IsValid(Type type)
        {
            return type.IsAssignableFrom(typeof(DateTime));
        }
    }
}
