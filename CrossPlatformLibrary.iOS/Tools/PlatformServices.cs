using System;
using System.Reflection;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Tools
{
    /// <summary>
    /// Provides an implementation of <see cref="IPlatformServices"/> for iOS.
    /// </summary>
    public class PlatformServices : PlatformServicesBase
    {
        public PlatformServices(ITracer tracer)
            : base(tracer)
        {
        }

        public override Assembly[] GetAssemblies()
        {
            // Overriding GetAssemblies with a more efficient and less error-prone implementation
            return AppDomain.CurrentDomain.GetAssemblies();
        }
    }
}
