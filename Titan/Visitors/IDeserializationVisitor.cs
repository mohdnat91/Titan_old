using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Visitors
{
    public interface IDeserializationVisitor
    {
        object Deserialize(DeserializationRequest request);
    }
}
