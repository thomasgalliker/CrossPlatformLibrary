using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class ContainerExtension : IContainerExtension
    {
        public void Initialize(ISimpleIoc container)
        {
            container.RegisterWithConvention<IPlatformSpecificExceptionHandler>();
        }
    }
}
