using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan
{
    public static class Conventions
    {
        public static string GetDefaultTagName(ResolutionRequest request)
        {
            if (request.Type == ResolutionType.Property)
            {
                return (request.Context["property"] as PropertyInfo).Name.ToLower();
            }
            else if (request.Type == ResolutionType.Collection)
            {
                return GetDefaultCollectionMemberTagName(request.Root);
            }

            return null;
        }

        private static string GetDefaultCollectionMemberTagName(XElement element)
        {
            string firstChildName = element.Elements().First().Name.LocalName;
            if (element.Elements().All(e => e.Name.LocalName == firstChildName))
            {
                return firstChildName;
            }
            return null;
        }

        public static Type GetDefaultImplementation(Type type)
        {
            if (type.IsGenericType)
            {
                Type generic = type.GetGenericTypeDefinition();
                if (generic == typeof(IList<>) || generic == typeof(ICollection<>) || generic == typeof(IEnumerable<>))
                {
                    return typeof(List<>).MakeGenericType(type.GetGenericArguments().Single());
                }
            }
            else
            {
                if (type == typeof(IEnumerable) || type == typeof(IList) || type == typeof(ICollection))
                {
                    return typeof(ArrayList);
                }
            }

            return null;
        }

        internal static System.Xml.XmlNodeType GetDefaultNodeType(ResolutionRequest request)
        {
            return System.Xml.XmlNodeType.Element;
        }
    }
}
