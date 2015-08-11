using System;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    internal interface IAdapterResolver
    {
        object Resolve(Type interfaceType, object[] args);

        Type ResolveClassType(Type interfaceType, bool throwIfNotFound = true);
    }
}