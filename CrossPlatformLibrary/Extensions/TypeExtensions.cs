using System;
using System.Linq;
using System.Reflection;

using Guards;

namespace CrossPlatformLibrary.Extensions
{
    public static class TypeExtensions
    {
        /// <summary>
        ///     Transforms the given type into a properly formatted type name.
        ///     Source: http://stackoverflow.com/questions/2448800/given-a-type-instance-how-to-get-generic-type-name-in-c
        /// </summary>
        /// <param name="type">The type from which the type name is extracted.</param>
        public static string ToTypeName(this Type type)
        {
            Guard.ArgumentNotNull(() => type);

            var typeInfo = type.GetTypeInfo();

            if (!typeInfo.IsGenericType)
            {
                return type.Name;
            }

            string genericTypeName = type.GetGenericTypeDefinition().Name;
            genericTypeName = genericTypeName.Substring(0, genericTypeName.IndexOf('`'));
            string genericArgs = string.Join(", ", typeInfo.GenericTypeArguments.Select(ToTypeName).ToArray());
            return genericTypeName + "<" + genericArgs + ">";
        }

        /// <summary>
        ///     Checks if the given type is a Nullable type.
        /// </summary>
        public static bool IsNullable(this Type type)
        {
            Guard.ArgumentNotNull(() => type);

            if (type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>))
            {
                return true;
            }

            return false;
        }

        // TODO GATH: CHeck this: http://stackoverflow.com/questions/2490244/default-value-of-a-type-at-runtime
        /// <summary>
        /// Returns the default value for the given type.
        /// </summary>
        /// <param name="type"></param>
        /// <returns>Value type: Default instance. Reference type: null.</returns>
        public static object GetDefaultValue(this Type type)
        {
            if (type.GetTypeInfo().IsValueType)
            {
                return Activator.CreateInstance(type);
            }

            return null;
        }
    }
}