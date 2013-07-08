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
    internal class CollectionItemResolutionHandler : IResolutionHandler
    {
        public ResolutionInfo Handle(ResolutionRequest request)
        {
            ResolutionInfo info = request.Conventions.GetDefaultResolution(request);

            XmlCollectionItemAttribute attribute = request.GetAttribute<XmlCollectionItemAttribute>();
            if (attribute != null)
            {
                if (!string.IsNullOrWhiteSpace(attribute.Name)) info.Predicate = (x => x.GetName() == attribute.Name);
                if (info.NodeType != XmlNodeType.None) info.NodeType = attribute.NodeType;
            }

            return info;
        }
    }
}
