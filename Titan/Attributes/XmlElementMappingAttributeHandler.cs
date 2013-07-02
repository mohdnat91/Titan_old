using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal class XmlElementMappingAttributeHandler : AbstractAttributeHandler<XmlElementMappingAttribute>
    {
        public override void Handle(ResolutionRequest request, ResolutionInfo info)
        {
            XmlElementMappingAttribute attribute = (XmlElementMappingAttribute) request.Attributes.Where(a => a.GetType() == typeof(XmlElementMappingAttribute)).Single();
            info.Name = attribute.TagName;
            info.NodeType = System.Xml.XmlNodeType.Element;
        }
    }
}
