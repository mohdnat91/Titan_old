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
        private Dictionary<ResolutionType, Func<ResolutionRequest, ResolutionInfo>> resolutions;

        public DefaultConventions()
        {
            resolutions = new Dictionary<ResolutionType, Func<ResolutionRequest, ResolutionInfo>>();
            resolutions.Add(ResolutionType.Property, GetPropertyResolution);
            resolutions.Add(ResolutionType.CollectionItem, GetCollectionItemResolution);
            resolutions.Add(ResolutionType.DictionaryKey, GetDictionaryKeyResoltion);
            resolutions.Add(ResolutionType.DictionaryValue, GetDictionaryValueResolution);
        }

        private ResolutionInfo GetDictionaryValueResolution(ResolutionRequest arg)
        {
            ResolutionInfo info = new ResolutionInfo();
            info.Predicate = null;
            info.NodeType = XmlNodeType.Text;
            return info;
        }

        private ResolutionInfo GetDictionaryKeyResoltion(ResolutionRequest arg)
        {
            ResolutionInfo info = new ResolutionInfo();
            info.Predicate = (x => x.GetName() == "key");
            info.NodeType = XmlNodeType.Attribute;
            return info;
        }

        private ResolutionInfo GetCollectionItemResolution(ResolutionRequest request)
        {
            ResolutionInfo info = new ResolutionInfo();

            XElement element = request.XRoot;

            string firstChildName = element.Elements().First().Name.LocalName;
            info.Predicate = (x => x.GetName() == firstChildName);

            info.NodeType = XmlNodeType.Element;
            return info;
        }

        private ResolutionInfo GetPropertyResolution(ResolutionRequest request)
        {
            ResolutionInfo info = new ResolutionInfo();
            string propName = ((PropertyInfo)request.Context["property"]).Name.ToLower();
            info.Predicate = (x => x.GetName() == propName);
            info.NodeType = XmlNodeType.Element;
            return info;
        }

        public ResolutionInfo GetDefaultResolution(ResolutionRequest request)
        {
            return resolutions[request.Type](request);
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
    }
}
