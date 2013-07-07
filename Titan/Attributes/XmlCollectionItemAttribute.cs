﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Titan.Attributes
{
    [AttributeUsage(AttributeTargets.Property, Inherited = false, AllowMultiple = true)]
    sealed public class XmlCollectionItemAttribute : Attribute
    {
        public string Name { get; set; }
        public XmlNodeType NodeType { get; set; }
        public int Level { get; set; }

        public XmlCollectionItemAttribute()
        {
            Name = null;
            NodeType = XmlNodeType.None;
            Level = 0;
        }
    }
}
