using System;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    internal interface IAdapterResolver
    {
        object Resolve(Type type, object[] args);
    }
}