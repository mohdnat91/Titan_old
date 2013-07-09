using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Titan.Deserializers;
using Titan.Utilities;
using Titan.Utilities.Exceptions;

namespace Titan.Visitors
{
    internal class DefaultDeserializationVisitor : IVisitor
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
        }

        public VisitorAction VisitType(Type type, Metadata metadata, out object result) {
            if (type == null) {
                throw new ArgumentNullException("type");
            }

            XObject xobject = metadata.Get<XObject>("xobject");

            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(type, xobject));
            if (deserializer == null)
            {
                result = Activator.CreateInstance(type);
                return VisitorAction.TraverseProperties;
            }

            try
            {
                result = deserializer.Handle(type, xobject, metadata);
                return VisitorAction.Stop;
            }
            catch
            {
                throw;
            }
        }

        public void VisitProperty(PropertyInfo property, Metadata metadata, ref object result) {
            if (property == null) {
                throw new ArgumentNullException("property");
            }

            Type type = property.PropertyType;

            ResolutionRequest res = new ResolutionRequest(ResolutionType.Property, metadata.Get<XElement>("xobject"), metadata);

            XObject xobject = XObjectMatcher.GetMatchingXObject(res);
            if (xobject == null) {
                if (type.IsNullable()) {
                    property.SetValue(result, null);
                    return;
                } else {
                    throw new NoMatchException(string.Format("Did not find a match for '{0}' property", property.Name));
                }
            }

            Metadata temp = new Metadata(metadata);
            temp.Set("xobject", xobject);


            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(type, xobject));
            if (deserializer == null) {
                object value = type.Accept(this, temp);
                property.SetValue(result, value);
                return;
            }

            try {
                object value = deserializer.Handle(type, xobject, temp);
                property.SetValue(result, value);
            } catch {
                throw;
            }
        }
    }
}
