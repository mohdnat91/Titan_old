using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titan.Parsers.Utilities
{
    public static class ParserRepository
    {
        private static List<IParser> parsers;        

        static ParserRepository()
        {
            parsers = new List<IParser>();
            parsers.Add(new IntParser());
            parsers.Add(new StringParser());
            parsers.Add(new BooleanParser());
            parsers.Add(new DateTimeParser());
            parsers.Add(new TimeSpanParser());
            parsers.Add(new DoubleParser());
            parsers.Add(new DecimalParser());
            parsers.Add(new CollectionParser());
            parsers.Add(new SectionParser());
        }

        public static object Parse(XElement element, string xpath, Type type)
        {
            IParser parser = parsers.Single(p => p.IsValid(type));
            return parser.Parse(element, xpath);
        }

        public static object Parse(XElement element, Type type)
        {
            IParser parser = parsers.Single(p => p.IsValid(type));
            return parser.Parse(element, null);
        }
    }
}
