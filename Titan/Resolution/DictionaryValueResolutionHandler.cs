using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Titan.Attributes;
using Titan.Utilities;

namespace Titan.Resolution
{
    internal class DictionaryValueResolutionHandler : IResolutionHandler
    {
        public ResolutionInfo Handle(ResolutionRequest request)
        {
            ResolutionInfo info = request.Metadata.Conventions.GetDefaultResolution(request);

            XmlDictionaryValueAttribute attribute = request.Metadata.Attribute<XmlDictionaryValueAttribute>();
            if (attribute != null)
            {
                if(!string.IsNullOrWhiteSpace(attribute.Name)) info.Predicate = (x => x.GetName() == attribute.Name);
                if(attribute.NodeType != XmlNodeType.None) info.NodeType = attribute.NodeType;
            }

            return info;
        }
    }
}
