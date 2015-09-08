
using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Tracing
{
    public class TracerFactoryConfiguration : IContainerExtension
    {
        public void Initialize(ISimpleIoc container)
        {
            Tracer.SetFactory(new AndroidLogTracerFactory());
        }
    }
}