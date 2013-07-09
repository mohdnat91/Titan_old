using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Conventions;
using Titan.Deserializers;
using Titan.Utilities;
using Titan.Visitors;

namespace Titan
{
    public class XmlDeserializer
    {
        public XDocument Document { get; private set; }

        public XmlDeserializer(string xml)
        {
            Document = XDocument.Parse(xml);
            HandleProcessingInstructions(Document.Nodes().Where(n => n is XProcessingInstruction));
        }

        private void HandleProcessingInstructions(IEnumerable<XNode> instructions)
        {
            return;
        }

        public T Deserialize<T>()
        {
            IConventions conventions = new DefaultConventions();
            IVisitor visitor = new DefaultDeserializationVisitor();
            Metadata metadata = new Metadata();
            metadata.Set("xobject", Document.Root);
            metadata.Visitor = visitor;
            metadata.Conventions = conventions;
            object value = typeof(T).Accept(visitor, metadata);
            return (T)value;
        }
    }
}
