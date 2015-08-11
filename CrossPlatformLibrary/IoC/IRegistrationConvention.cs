using System;
using System.Reflection;

namespace CrossPlatformLibrary.IoC
{
    /// <summary>
    /// IRegistrationConvention interface is used to define conversion rules
    /// to convert platform-agnostics to platform-specifics.
    /// </summary>
    public interface IRegistrationConvention
    {
        /// <summary>
        /// Transforms a platform-agnostic assembly name into a platform-specific assembly name.
        /// </summary>
        /// <param name="assemblyName">The assembly name of the platform-agnostic assembly.</param>
        /// <returns>The assembly name of the platform-specific assembly as a string.</returns>
        string PlatformNamingConvention(AssemblyName assemblyName);

        /// <summary>
        /// Transforms a platform-agnostic interface into a platform-specific type name.
        /// </summary>
        /// <param name="interfaceType"></param>
        /// <returns></returns>
        string InterfaceToClassNamingConvention(Type interfaceType);
    }
}