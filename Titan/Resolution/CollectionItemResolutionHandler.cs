using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Attributes;
using Titan.Utilities;

namespace Titan.Resolution
{
    internal class CollectionItemResolutionHandler : IResolutionHandler
    {
        public ResolutionInfo Handle(ResolutionRequest request)
        {
            XmlCollectionItemAttribute attribute = request.Attribute<XmlCollectionItemAttribute>();
            if (attribute != null)
            {
                ResolutionInfo info = new ResolutionInfo();
                info.Predicate = (x => x.GetName() == attribute.Name);
                info.NodeType = attribute.NodeType;
                return info;
            }

            return request.Conventions.GetDefaultResolution(request);
        }
    }
}
