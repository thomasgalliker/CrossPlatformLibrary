namespace CrossPlatformLibrary.IoC
{
    /// <summary>
    /// Instances of IContainerExtension are detected via reflection at bootstrapping time.
    /// This interface is intended to set-up the IoC container of library projects.
    /// </summary>
    public interface IContainerExtension
    {
        /// <summary>
        /// Allows to set-up the given container with plugin-specific dependencies. 
        /// </summary>
        /// <param name="container"></param>
        void Initialize(ISimpleIoc container);
    }
}