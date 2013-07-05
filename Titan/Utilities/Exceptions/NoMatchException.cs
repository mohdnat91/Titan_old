using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Titan.Utilities.Exceptions
{
    [Serializable]
    public class NoMatchException : TitanException
    {
        public NoMatchException() { }

        public NoMatchException(string message) : base(message)
        {
        }
    }
}
