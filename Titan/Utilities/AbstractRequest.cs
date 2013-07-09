using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Conventions;

namespace Titan.Utilities
{
    public abstract class AbstractRequest
    {
        public virtual XObject XRoot { get; set; }
        public Dictionary<string, object> Context { get; set; }

        public T Get<T>(string key)
        {
            if (Context.ContainsKey(key))
            {
                return (T)Context[key];
            }
            return default(T);
        }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                return Get<IEnumerable<Attribute>>("attributes") ?? Enumerable.Empty<Attribute>();
            }

            set
            {
                Context["attributes"] = value;
            }
        }

        public IConventions Conventions
        {
            get
            {
                return Get<IConventions>("conventions");
            }

            set
            {
                Context["conventions"] = value;
            }
        }

        public T GetAttribute<T>() where T : Attribute
        {
            return GetAttributes<T>().SingleOrDefault();
        }

        public IEnumerable<T> GetAttributes<T>() where T : Attribute
        {
            return Attributes.Where(a => a is T).Select(a => a as T);
        }

        public AbstractRequest()
        {
            Context = new Dictionary<string, object>();
        }

        public AbstractRequest(Dictionary<string, object> context)
        {
            Context = new Dictionary<string,object>(context);
        }
    }
}
