using System;

namespace CrossPlatformLibrary.Bootstrapping
{
    /// <summary>
    /// IBootstrapper is the application lifecycle manager and the entry point
    /// of any application built with CrossPlatformLibrary.
    /// </summary>
    public interface IBootstrapper : IDisposable
    {
        /// <summary>
        /// Returns the current application lifecycle state.
        /// </summary>
        ApplicationLifecycle ApplicationLifecycle { get; }

        /// <summary>
        /// Runs the startup procedure of the bootstrapper.
        /// </summary>
        /// <exception cref="BootstrappingException">An unknown exception occurred during the startup process.</exception>
        void Startup();

        void Sleep();

        /// <summary>
        /// Runs the resume procedure in order to wake-up a sleeping application.
        /// </summary>
        void Resume();

        /// <summary>
        /// Runs the shutdown procedure.
        /// </summary>
        void Shutdown();
    }
}