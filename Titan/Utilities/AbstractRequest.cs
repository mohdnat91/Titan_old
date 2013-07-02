﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titan.Utilities
{
    public abstract class AbstractRequest
    {
        public virtual XObject Root { get; set; }
        public Dictionary<string, object> Context { get; set; }

        public IEnumerable<Attribute> Attributes
        {
            get
            {
                if (Context.ContainsKey("attributes"))
                    return Context["attributes"] as IEnumerable<Attribute>;
                else
                    return Enumerable.Empty<Attribute>();
            }

            set
            {
                Context["attributes"] = value;
            }
        }

        public AbstractRequest()
        {
            Context = new Dictionary<string, object>();
        }

    }
}
