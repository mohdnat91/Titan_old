using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Attributes
{
    internal abstract class AbstractAttributeHandler<T> : IAttributeHandler where T : Attribute
    {
        public bool CanHandle(ResolutionRequest request)
        {
            return request.Attributes.Any(a => a.GetType() == typeof(T));
        }

        public abstract void Handle(ResolutionRequest request, ResolutionInfo info);
    }
}
