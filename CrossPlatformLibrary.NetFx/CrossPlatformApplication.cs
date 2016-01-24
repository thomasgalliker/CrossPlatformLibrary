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
            this.bootstrapper.Startup();
        }

        protected override void OnExit(ExitEventArgs e)
        {
            this.bootstrapper.Shutdown();
            base.OnExit(e);
        }
    }
}
