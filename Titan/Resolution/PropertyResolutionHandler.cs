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
            ResolutionInfo info = request.Conventions.GetDefaultResolution(request);

            XmlElementAttribute element = request.Attribute<XmlElementAttribute>();
            if (element != null)
            {
                info.NodeType = XmlNodeType.Element;
                if(element.ElementName != null) info.Predicate = (x => x.GetName() == element.ElementName);
            }

            XmlAttributeAttribute attribute = request.Attribute<XmlAttributeAttribute>();
            if (attribute != null)
            {
                info.NodeType = XmlNodeType.Attribute;
                if(attribute.AttributeName != null) info.Predicate = (x => x.GetName() == attribute.AttributeName);
            }

            return info;
        }

    }
}
