using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Utilities;

namespace Titan.Deserializers
{
    internal class BasicTypeDeserializer : ITypeDeserializer
    {
        private Type basicType;
        private Type rootType;
        private Delegate converter;

        public BasicTypeDeserializer(Type type, Type rootType, Delegate converter)
        {
            this.basicType = type;
            this.rootType = rootType;
            this.converter = converter;
        }

        public bool CanHandle(DeserializationRequest request)
        {
            return request.TargetType == basicType && request.Root.GetType() == rootType;
        }

        public object Handle(DeserializationRequest request)
        {
            return converter.DynamicInvoke(request.Root);
        }
    }
}
