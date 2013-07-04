using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal class XmlAttributeAttributeHandler : AbstractAttributeHandler<XmlAttributeAttribute>
    {
        public override void Handle(ResolutionRequest request, ResolutionInfo info)
        {
            XmlAttributeAttribute attribute = (XmlAttributeAttribute)request.Attributes.Where(a => a.GetType() == typeof(XmlAttributeAttribute)).Single();
            info.Name = attribute.AttributeName;
            info.NodeType = System.Xml.XmlNodeType.Attribute;
        }
    }
}
