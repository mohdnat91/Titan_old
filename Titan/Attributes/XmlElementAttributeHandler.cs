using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal class XmlElementAttributeHandler : AbstractAttributeHandler<XmlElementAttribute>
    {
        public override void Handle(ResolutionRequest request, ResolutionInfo info)
        {
            XmlElementAttribute attribute = (XmlElementAttribute)request.Attributes.Where(a => a.GetType() == typeof(XmlElementAttribute)).Single();
            info.Name = attribute.ElementName;
            info.NodeType = XmlNodeType.Element;
        }
    }
}
