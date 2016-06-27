using System;

namespace CrossPlatformLibrary.IoC
{
    public interface IResolvedParameter
    {
        Type Type { get; }

        string Key { get; }
    }
}