using System;

using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    internal interface IAdapterResolver
    {
        object Resolve(Type interfaceType, bool throwIfNotFound, object[] args);

        Type ResolveClassType(Type interfaceType, bool throwIfNotFound = true);

        IRegistrationConvention RegistrationConvention { get; set; }
    }
}