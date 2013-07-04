using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal class XmlCollectionItemAttributeHandler : AbstractAttributeHandler<XmlCollectionItemAttribute>
    {
        public override void Handle(ResolutionRequest request, ResolutionInfo info)
        {
            XmlCollectionItemAttribute attribtue = (XmlCollectionItemAttribute)request.Attributes.Single(a => a is XmlCollectionItemAttribute);
            if (request.Type == ResolutionType.CollectionMember)
            {
                info.Name = attribtue.ItemName;
                info.NodeType = attribtue.NodeType;
            }
        }
    }
}
