using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.IntegrationTests.Dispatching;

using Xunit.Runners.UI;

namespace CrossPlatformLibrary.WindowsPhone8.Tests
{
    public partial class MainPage : RunnerApplicationPage
    {
        public MainPage()
        {
            this.InitializeComponent();
        }

        protected override void OnInitializeRunner()
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            this.AddTestAssembly(typeof(DispatcherServiceTests).Assembly);
        }
    }
}