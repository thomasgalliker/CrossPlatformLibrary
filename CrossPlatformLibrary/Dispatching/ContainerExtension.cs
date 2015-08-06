using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Dispatching
{
    public class ContainerExtension : IContainerExtension
    {
        public void Initialize(ISimpleIoc container)
        {
            container.RegisterPlatformSpecific<IDispatcherService>();
        }
    }
}
