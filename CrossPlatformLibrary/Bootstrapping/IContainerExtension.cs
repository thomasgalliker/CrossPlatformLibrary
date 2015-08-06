using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Bootstrapping
{
    public interface IContainerExtension
    {
        /// <summary>
        /// Allows to set-up the given container with plugin-specific dependencies. 
        /// </summary>
        /// <param name="container"></param>
        void Initialize(ISimpleIoc container);
    }
}