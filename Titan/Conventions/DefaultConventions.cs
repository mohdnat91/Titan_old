using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Conventions
{
    public class DefaultConventions : IConventions
    {
        public string GetDefaultXObjectName(AbstractRequest request)
        {
            ResolutionRequest req = request as ResolutionRequest;
            if (req.Type == ResolutionType.Property)
            {
                return (request.Context["property"] as PropertyInfo).Name.ToLower();
            }
            else if (req.Type == ResolutionType.CollectionMember)
            {
                return GetDefaultCollectionMemberTagName(req.Root);
            }
            else if (req.Type == ResolutionType.DictionaryKey)
            {
                return "key";
            }
            else if (req.Type == ResolutionType.DictionaryValue)
            {
                return req.Root.Name.LocalName;
            }

            return null;
        }

        public Type GetDefaultInterfaceImplementation(AbstractRequest request)
        {
            Type type = (request as DeserializationRequest).TargetType;
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

        public XmlNodeType GetDefaultNodeType(AbstractRequest request)
        {
            if (request is ResolutionRequest && ((ResolutionRequest)request).Type == ResolutionType.DictionaryKey)
            {
                return XmlNodeType.Attribute;
            }
            if (request is ResolutionRequest && ((ResolutionRequest)request).Type == ResolutionType.DictionaryValue)
            {
                return XmlNodeType.Text;
            }
            return XmlNodeType.Element;
        }

        private string GetDefaultCollectionMemberTagName(XElement element)
        {
            string firstChildName = element.Elements().First().Name.LocalName;
            if (element.Elements().All(e => e.Name.LocalName == firstChildName))
            {
                return firstChildName;
            }
            return null;
        }
    }
}
