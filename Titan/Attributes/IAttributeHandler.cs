using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Attributes
{
    public interface IAttributeHandler
    {
        bool CanHandle(ResolutionRequest request);
        void Handle(ResolutionRequest request, ResolutionInfo info);
    }
}
