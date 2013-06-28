using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titan.Parsers
{
    public interface IParser
    {
        bool IsValid(Type type);
        object Parse(XElement element, string xpath);
    }

    public abstract class AbstractParser<T> : IParser
    {
        public abstract T Parse(XElement element, string xpath);

        public abstract bool IsValid(Type type);

        object IParser.Parse(XElement element, string xpath)
        {
            XNode d;
            return Parse(element, xpath);
        }

    }
}
