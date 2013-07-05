using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Attributes;
using Titan.Utilities;

namespace Titan.Resolution
{
    internal class DictionaryKeyResolutionHandler : IResolutionHandler
    {
        public ResolutionInfo Handle(ResolutionRequest request)
        {
            XmlDictionaryEntryAttribute attribute = request.Attribute<XmlDictionaryEntryAttribute>();
            if (attribute != null)
            {
                ResolutionInfo info = new ResolutionInfo();
                info.Predicate = (x => x.GetName() == attribute.KeyName);
                info.NodeType = attribute.KeyNodeType;
                return info;
            }

            return request.Conventions.GetDefaultResolution(request);
        }
    }
}
