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
            ResolutionInfo info = new ResolutionInfo();
            XmlElementAttribute element = request.Attribute<XmlElementAttribute>();
            if (element != null)
            {
                info.NodeType = XmlNodeType.Element;
                info.Predicate = (x => x.GetName() == element.ElementName);
                return info;
            }

            XmlAttributeAttribute attribute = request.Attribute<XmlAttributeAttribute>();
            if (attribute != null)
            {
                info.NodeType = XmlNodeType.Attribute;
                info.Predicate = (x => x.GetName() == attribute.AttributeName);
                return info;
            }

            return request.Conventions.GetDefaultResolution(request);
        }

    }
}
