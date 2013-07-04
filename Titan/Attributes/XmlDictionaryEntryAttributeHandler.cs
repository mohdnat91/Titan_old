using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal class XmlDictionaryEntryAttributeHandler : AbstractAttributeHandler<XmlDictionaryEntryAttribute>
    {
        public override void Handle(ResolutionRequest request, ResolutionInfo info)
        {
            XmlDictionaryEntryAttribute attribtue = (XmlDictionaryEntryAttribute)request.Attributes.Single(a => a is XmlDictionaryEntryAttribute);
            if (request.Type == ResolutionType.DictionaryKey)
            {
                info.Name = attribtue.KeyName;
                info.NodeType = attribtue.KeyNodeType;
            }
            else if (request.Type == ResolutionType.DictionaryValue)
            {
                info.Name = attribtue.ValuName;
                info.NodeType = attribtue.ValueNodeType;
            }
        }
    }
}
