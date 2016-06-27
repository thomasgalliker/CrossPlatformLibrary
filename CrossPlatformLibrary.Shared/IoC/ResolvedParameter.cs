using System;

namespace CrossPlatformLibrary.IoC
{
    public class ResolvedParameter<T> : IResolvedParameter
    {
        public ResolvedParameter(string key)
        {
            this.Key = key;
            this.Type = typeof(T);
        }

        public Type Type { get; private set; }

        public string Key { get; private set; }
    }
}