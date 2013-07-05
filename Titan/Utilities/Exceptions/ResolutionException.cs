using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Utilities.Exceptions
{
    [Serializable]
    public class ResolutionException : TitanException
    {
        public ResolutionException() { }
        public ResolutionException(string message) : base(message) { }
        public ResolutionException(string message, Exception inner) : base(message, inner) { }
        protected ResolutionException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
