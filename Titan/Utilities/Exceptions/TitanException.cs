using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Utilities.Exceptions
{
    [Serializable]
    public class TitanException : Exception
    {
        public TitanException() { }
        public TitanException(string message) : base(message) { }
        public TitanException(string message, Exception inner) : base(message, inner) { }
        protected TitanException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
