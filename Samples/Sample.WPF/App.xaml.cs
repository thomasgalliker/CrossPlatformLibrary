using System.Windows;

using CrossPlatformLibrary.Bootstrapping;

namespace Sample.WPF
{
    public partial class App : Application
    {
        private readonly Bootstrapper bootstrapper;

        public App()
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
