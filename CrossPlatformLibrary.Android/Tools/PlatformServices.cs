using System;
using System.Reflection;
using CrossPlatformLibrary.Tracing;

namespace CrossPlatformLibrary.Tools
{
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

        public override void LoadAssemblies()
        {
            ////throw new NotImplementedException("Is it really possible/necessary on Android?");
        }
    }
}
