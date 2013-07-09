using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Titan.Visitors;

namespace Titan.Utilities
{
    public static class TypeExtensions
    {
        public static bool IsAssignableToGenericType(this Type givenType, Type genericType)
        {
            if (!genericType.IsGenericTypeDefinition) return false;

            if (givenType.IsGenericType && givenType.GetGenericTypeDefinition() == genericType)
                return true;

            foreach (Type it in givenType.GetInterfaces())
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return true;
            }

            Type baseType = givenType.BaseType;
            if (baseType == null) return false;

            return IsAssignableToGenericType(baseType, genericType);
        }

        public static Type[] GetParentTypeParameters(this Type givenType, Type genericType)
        {
            if (!genericType.IsGenericTypeDefinition) throw new InvalidOperationException("genericType parameter must be a generic type defenition");

            foreach (Type it in givenType.GetInterfaces())
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return it.GetGenericArguments();
            }

            Type baseType = givenType.BaseType;
            if (baseType == null) return null;

            return GetParentTypeParameters(baseType, genericType);

        }

        public static bool IsNullable(this Type type)
        {
            return type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        public static object Accept(this Type type, IVisitor visitor, Metadata metadata) {
            object result;
            VisitorAction action = visitor.VisitType(type, metadata, out result);

            if (action == VisitorAction.Stop) return result;

            foreach (PropertyInfo property in type.GetProperties().Where(p => p.CanWrite)) {
                Metadata propMetadata = new Metadata(metadata);
                propMetadata.Set("attributes", property.GetCustomAttributes<Attribute>());
                propMetadata.Set("property", property);
                visitor.VisitProperty(property, propMetadata, ref result);
            }

            return result;
        }
    }
}
