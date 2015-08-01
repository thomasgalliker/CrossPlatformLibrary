
using System;

namespace CrossPlatformLibrary.IoC
{
    /// <summary>
    /// When used with the SimpleIoc container, this annotation can be used 
    /// to mark interfaces which shall not be cached in the instances cache of SimpleIoc.
    /// Every attempt to construct a dependency of a marked interface will end up
    /// in creating a new object from scratch.
    /// </summary>
    [AttributeUsage(AttributeTargets.Interface)]
    public sealed class UseCacheAttribute : Attribute
    {
        private readonly bool useCache;

        public UseCacheAttribute(bool useCache)
        {
            this.useCache = useCache;
        }

        public bool UseCache
        {
            get{ return this.useCache; }
        }
    }
}