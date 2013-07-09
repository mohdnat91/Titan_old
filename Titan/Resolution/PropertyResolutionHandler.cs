using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Titan.Utilities;

namespace Titan.Resolution
{
    internal class PropertyResolutionHandler : IResolutionHandler
    {
        
        public ResolutionInfo Handle(ResolutionRequest request)
        {
            ResolutionInfo info = request.Metadata.Conventions.GetDefaultResolution(request);

            XmlElementAttribute element = request.Metadata.Attribute<XmlElementAttribute>();
            if (element != null)
            {
                info.NodeType = XmlNodeType.Element;
                if(!string.IsNullOrWhiteSpace(element.ElementName)) info.Predicate = (x => x.GetName() == element.ElementName);
            }

            XmlAttributeAttribute attribute = request.Metadata.Attribute<XmlAttributeAttribute>();
            if (attribute != null)
            {
                info.NodeType = XmlNodeType.Attribute;
                if(!string.IsNullOrWhiteSpace(attribute.AttributeName)) info.Predicate = (x => x.GetName() == attribute.AttributeName);
            }

            return info;
        }

    }
}
