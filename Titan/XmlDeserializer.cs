using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Conventions;
using Titan.Deserializers;
using Titan.Utilities;

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
            DeserializationRequest request = new DeserializationRequest() { TargetType = typeof(T), Root = Document.Root };
            request.Conventions = new DefaultConventions();
            object value = DeserializationUtilities.Deserialize(request);
            return (T)value;
        }
    }
}
