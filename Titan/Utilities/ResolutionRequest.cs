using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Titan.Utilities
{
    public class ResolutionRequest
    {
        public Metadata Metadata { get; set; }
        public ResolutionType Type { get; set; }
        public XElement Root { get; set; }

        public ResolutionRequest(ResolutionType type, XElement root, Metadata metadata)
        {
            Root = root;
            Type = type;
            Metadata = metadata;
        }
    }
}
