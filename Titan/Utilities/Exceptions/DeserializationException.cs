using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Utilities.Exceptions
{
    [Serializable]
    public class DeserializationException : TitanException
    {
        public DeserializationException() { }
        public DeserializationException(string message) : base(message) { }
        public DeserializationException(string message, Exception inner) : base(message, inner) { }
        protected DeserializationException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
