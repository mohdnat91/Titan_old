using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Conventions;
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
                return Get<IDeserializationVisitor>("deserialization_visitor");
            }
            set
            {
                Context["deserialization_visitor"] = value;
            }

        }

        public DeserializationRequest(XObject xroot, Type target, IDeserializationVisitor visitor, IConventions conventions)
        {
            XRoot = xroot;
            TargetType = target;
            Visitor = visitor;
            Conventions = conventions;
        }

        public DeserializationRequest(XObject xroot, Type target, Dictionary<string, object> context) : base(context)
        {
            XRoot = xroot;
            TargetType = target;
        }
    }
}
