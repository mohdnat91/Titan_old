using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Titan.Utilities;

namespace Titan.Conventions
{
    public interface IConventions
    {
        string GetDefaultXObjectName(AbstractRequest request);
        Type GetDefaultInterfaceImplementation(AbstractRequest request);
        XmlNodeType GetDefaultNodeType(AbstractRequest request);
    }
}
