using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Titan.Parsers;
using Titan.Parsers.Utilities;

namespace Titan
{

    public class Configuration
    {
        private XDocument configDom;
        private Dictionary<Type, object> cache;

        public Configuration(string FileName)
        {
            configDom = XDocument.Load(FileName);
        }

        public T Get<T>()
        {
            return (T) ParserRepository.Parse(configDom.Root, typeof(T));
        }

    }
}
