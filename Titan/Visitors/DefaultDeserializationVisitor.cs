using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Titan.Deserializers;
using Titan.Utilities;
using Titan.Utilities.Exceptions;

namespace Titan.Visitors
{
    internal class DefaultDeserializationVisitor : IDeserializationVisitor
    {
        private static List<ITypeDeserializer> deserializers;

        public DefaultDeserializationVisitor()
        {
            deserializers = new List<ITypeDeserializer>();
            deserializers.Add(new EnumDeserializer());
            deserializers.Add(new InterfaceDeserializer());
            deserializers.Add(new ArrayDeserializer());
            deserializers.Add(new PrimitiveTypeDeserializer());
            deserializers.Add(new KeyValuePairDeserializer());
            deserializers.Add(new DictionaryDeserializer());
            deserializers.Add(new GenericListDeserializer());
            deserializers.Add(new NonGenericListDeserializer());
            deserializers.Add(new ComplexTypeDeserializer());
        }

        public object Deserialize(DeserializationRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException("request");
            }

            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(request));
            if (deserializer == null)
            {
                throw new DeserializationException("Cannot find a handler for the deserialization request");
            }

            try
            {
                return deserializer.Handle(request);
            }
            catch
            {
                throw;
            }
        }
    }
}
