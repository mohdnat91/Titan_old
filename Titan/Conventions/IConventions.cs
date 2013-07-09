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
        ResolutionInfo GetDefaultResolution(ResolutionRequest request);
        Type GetDefaultInterfaceImplementation(Type type, Metadata metadata);
    }
}
