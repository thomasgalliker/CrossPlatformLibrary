using System;
using System.Reflection;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.IoC
{
    /// <summary>
    /// DefaultRegistrationConvention is a registration convention which adds the string ".Platform"
    /// to the platform-agnostic assembly name in order to resolve the platform-specific assembly.
    /// Furthermore, platform-specific types are made of interface type name excluding the leading string "I".
    /// </summary>
    public class DefaultRegistrationConvention : IRegistrationConvention
    {
        public virtual string PlatformNamingConvention(AssemblyName assemblyName)
        {
            return string.Format("{0}.Platform", assemblyName.Name);
        }

        public virtual string InterfaceToClassNamingConvention(Type interfaceType)
        {
            Guard.ArgumentIsTrue(() => interfaceType.GetTypeInfo().IsInterface);
            Guard.ArgumentIsTrue(() => interfaceType.DeclaringType == null);
            Guard.ArgumentIsTrue(() => interfaceType.Name.StartsWith("I", StringComparison.Ordinal));

            return string.Format("{0}.{1}", interfaceType.Namespace, interfaceType.Name.Substring(1));
        }
    }
}
