using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Visitors;

namespace Titan.Utilities
{
    public class DeserializationRequest : AbstractRequest
    {
        public Type TargetType { get; set; }
        public IDeserializationVisitor Visitor
        {
            get
            {
                return Context.ContainsKey("deserialization_visitor") ? (IDeserializationVisitor) Context["deserialization_visitor"] : null;
            }
            set
            {
                Context["deserialization_visitor"] = value;
            }

        }
    }
}
