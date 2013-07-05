using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Attributes;
using Titan.Utilities;

namespace Titan.Resolution
{
    internal class DictionaryValueResolutionHandler : IResolutionHandler
    {
        public ResolutionInfo Handle(ResolutionRequest request)
        {
            XmlDictionaryEntryAttribute attribute = request.Attribute<XmlDictionaryEntryAttribute>();
            if (attribute != null)
            {
                ResolutionInfo info = new ResolutionInfo();
                info.Predicate = (x => x.GetName() == attribute.ValueName);
                info.NodeType = attribute.ValueNodeType;
                return info;
            }

            return request.Conventions.GetDefaultResolution(request);
        }
    }
}
