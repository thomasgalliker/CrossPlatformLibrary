using System;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    internal interface IAdapterResolver
    {
        ////object Resolve(Type interfaceType, object[] args); TODO GATH: Can be removed in code

        Type ResolveClassType(Type interfaceType, bool throwIfNotFound = true);
    }
}