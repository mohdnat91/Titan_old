using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal class XmlAttributeMappingAttributeHandler : AbstractAttributeHandler<XmlAttributeMappingAttribute>
    {
        public override void Handle(ResolutionRequest request, ResolutionInfo info)
        {
            XmlAttributeMappingAttribute attribute = (XmlAttributeMappingAttribute)request.Attributes.Where(a => a.GetType() == typeof(XmlAttributeMappingAttribute)).Single();
            info.Name = attribute.Name;
            info.NodeType = System.Xml.XmlNodeType.Attribute;
        }
    }
}
