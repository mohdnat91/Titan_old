using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Conventions;
using Titan.Visitors;

namespace Titan.Utilities
{
    public class Metadata
    {
        private Dictionary<string, object> Context;

        public Metadata() {
            Context = new Dictionary<string, object>();
        }

        public Metadata(Metadata other) {
            Context = new Dictionary<string, object>(other.Context);
        }

        public T Get<T>(string key) {
            if (Context.ContainsKey(key)) {
                return (T)Context[key];
            }
            return default(T);
        }

        public void Set(string key, object value) {
            Context[key] = value;
        }

        public IEnumerable<T> Attributes<T>() where T : Attribute {
            var atts = Get<IEnumerable<Attribute>>("attributes");
            if(atts == null) return Enumerable.Empty<T>();
            return atts.Where(a => a is T).Select(a => a as T);
        }

        public T Attribute<T>() where T : Attribute {
            return Attributes<T>().SingleOrDefault();
        }

        public IVisitor Visitor { get { return Get<IVisitor>("visitor"); } set { Set("visitor", value); } }
        public IConventions Conventions { get { return Get<IConventions>("conventions"); } set { Set("conventions", value); } }
    }
}
