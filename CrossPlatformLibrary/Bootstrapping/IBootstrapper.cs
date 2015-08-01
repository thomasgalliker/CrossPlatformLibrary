namespace CrossPlatformLibrary.Bootstrapping
{
    public interface IBootstrapper
    {
        /// <summary>
        /// Runs the startup procedure of the bootstrapper.
        /// </summary>
        /// <exception cref="BootstrappingException">An unknown exception occurred during the startup process.</exception>
        void Startup();

        /// <summary>
        /// Runs the shutdown procedure of the bootstrapper.
        /// </summary>
        void Shutdown();
    }
}