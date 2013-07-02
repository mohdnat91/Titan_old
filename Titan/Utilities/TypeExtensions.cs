using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public static Type GetParentTypeParameter(this Type givenType, Type genericType)
        {
            if (!genericType.IsGenericTypeDefinition) throw new InvalidOperationException("genericType parameter must be a generic type defenition");

            foreach (Type it in givenType.GetInterfaces())
            {
                if (it.IsGenericType && it.GetGenericTypeDefinition() == genericType)
                    return it.GetGenericArguments().Single();
            }

            Type baseType = givenType.BaseType;
            if (baseType == null) return null;

            return GetParentTypeParameter(baseType, genericType);

        }
    }
}
