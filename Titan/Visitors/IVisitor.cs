using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Titan.Utilities;

namespace Titan.Visitors
{
    public interface IVisitor
    {
        VisitorAction VisitType(Type type, Metadata metadata, out object result);
        void VisitProperty(PropertyInfo property, Metadata metadata, ref object result);
    }
}
