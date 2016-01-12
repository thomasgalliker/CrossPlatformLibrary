using CrossPlatformLibrary.Bootstrapping;

using Xamarin.Forms;

namespace Sample
{
    public class App : Application
    {
        private readonly Bootstrapper bootstrapper;

        public App()
        {
            this.bootstrapper = new Bootstrapper();

            this.MainPage = new DemoPage();
        }

        protected override void OnStart()
        {
            this.bootstrapper.Startup();
        }

        protected override void OnSleep()
        {
            this.bootstrapper.Sleep();
        }

        protected override void OnResume()
        {
            this.bootstrapper.Resume();
        }
    }
}