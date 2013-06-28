using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Titan.Parsers.Utilities;

namespace Titan.Parsers
{
    public class CollectionParser : AbstractParser<IList>
    {
        Type collectionType;

        public override bool IsValid(Type type)
        {
            if (typeof(IList).IsAssignableFrom(type))
            {
                collectionType = type;
                return true;
            }
            
            return false;
        }

        public override IList Parse(XElement element, string xpath)
        {
            IEnumerable<XElement> nodes = element.XPathSelectElements(xpath);
            IList collection = (IList) Activator.CreateInstance(collectionType);

            Type subType = typeof(string);

            if(collectionType.IsGenericType){
                subType = collectionType.GetGenericArguments().Single();
            }

            foreach (XElement e in nodes)
            {
                collection.Add(ParserRepository.Parse(e, subType));
            }
            
            return collection;
        }

    }
}
