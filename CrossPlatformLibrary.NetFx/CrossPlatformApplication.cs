using System.Windows;

using CrossPlatformLibrary.Bootstrapping;

namespace CrossPlatformLibrary
{
    /// <summary>
    /// CrossPlatformApplication manages the bootstrapping events of this application.
    /// </summary>
    public partial class CrossPlatformApplication : Application
    {
        private readonly Bootstrapper bootstrapper;

        protected CrossPlatformApplication()
        {
            this.bootstrapper = new Bootstrapper();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            this.bootstrapper.Startup();
            base.OnStartup(e);
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.bootstrapper.Shutdown();
            base.OnExit(e);
        }
    }
}
