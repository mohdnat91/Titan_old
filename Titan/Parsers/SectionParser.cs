using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using System.Xml.XPath;
using Titan.Parsers.Utilities;

namespace Titan.Parsers
{
    public class SectionParser : AbstractParser<object>
    {
        Type sectionType;

        public override object Parse(XElement element, string xpath)
        {
            object section = Activator.CreateInstance(sectionType);

            XElement root = xpath != null ? element.XPathSelectElements(xpath).Single() : element.XPathSelectElements(sectionType.GetCustomAttribute<SectionAttribute>().XPath).Single();

            IEnumerable<PropertyInfo> properties = sectionType.GetProperties().Where(p => p.CanWrite);

            foreach (PropertyInfo property in properties.Where(p => p.GetCustomAttribute<XmlElementMappingAttribute>() != null))
            {
                XmlElementMappingAttribute attribute = property.GetCustomAttribute<XmlElementMappingAttribute>();
                Type propType = property.PropertyType;
                object value = ParserRepository.Parse(root, attribute.XPath, propType);
                property.SetValue(section, value);
            }

            foreach (PropertyInfo property in properties.Where(p => p.GetCustomAttribute<XmlAttributeMappingAttribute>() != null))
            {
                Console.WriteLine("Attribute: " + property.Name);
            }

            return section;
        }

        public override bool IsValid(Type type)
        {
            if (type.GetCustomAttribute<SectionAttribute>() != null)
            {
                sectionType = type;
                return true;
            }
            return false;
        }
    }
}
