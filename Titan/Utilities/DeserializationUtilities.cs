using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Titan.Attributes;
using Titan.Deserializers;

namespace Titan.Utilities
{
    public static class DeserializationUtilities
    {
        private static List<ITypeDeserializer> deserializers;
        private static List<IAttributeHandler> attributeHandlers;

        static DeserializationUtilities()
        {
            deserializers = new List<ITypeDeserializer>();
            deserializers.Add(new EnumDeserializer());
            deserializers.Add(new InterfaceDeserializer());
            deserializers.AddRange(typeof(XElement).GetMethods().Where(m => m.Name == "op_Explicit").Select(m => createDelegate(m, typeof(XElement))).ToList());
            deserializers.AddRange(typeof(XAttribute).GetMethods().Where(m => m.Name == "op_Explicit").Select(m => createDelegate(m, typeof(XAttribute))).ToList());
            deserializers.Add(new GenericListDeserializer());
            deserializers.Add(new NonGenericListDeserializer());
            deserializers.Add(new ComplexTypeDeserializer());

            attributeHandlers = new List<IAttributeHandler>();
            attributeHandlers.Add(new XmlElementMappingAttributeHandler());
            attributeHandlers.Add(new XmlAttributeMappingAttributeHandler());
        }

        public static object Deserialize(DeserializationRequest request)
        {
            ITypeDeserializer deserializer = deserializers.FirstOrDefault(d => d.CanHandle(request));
            return deserializer.Handle(request);
        }  

        public static IEnumerable<XObject> GetMatchingXObjects(ResolutionRequest request)
        {
            ResolutionInfo info = new ResolutionInfo();

            attributeHandlers.Where(h => h.CanHandle(request)).ToList().ForEach(h => h.Handle(request, info));

            string name = GetTagName(request, info);

            if (info.NodeType == XmlNodeType.None)
            {
                info.NodeType = Conventions.GetDefaultNodeType(request);
            }

            if (info.NodeType == XmlNodeType.Attribute)
            {
                return request.Root.Attributes(name);
            }
            else if (info.NodeType == XmlNodeType.Element)
            {
                return request.Root.Elements(name);
            }

            return null;
        }

        private static string GetTagName(ResolutionRequest request, ResolutionInfo info)
        {
            if (info.Name != null)
            {
                return info.Name;
            }
            else
            {
                return Conventions.GetDefaultTagName(request);
            }
        }

        public static XObject GetMatchingXObject(ResolutionRequest request)
        {
            return GetMatchingXObjects(request).Single();
        }

        private static ITypeDeserializer createDelegate(MethodInfo method, Type type)
        {
            Type delegateType = typeof(Func<,>).MakeGenericType(type, method.ReturnType);
            return new BasicTypeDeserializer(method.ReturnType, type, Delegate.CreateDelegate(delegateType, method));
        }
    }
}
