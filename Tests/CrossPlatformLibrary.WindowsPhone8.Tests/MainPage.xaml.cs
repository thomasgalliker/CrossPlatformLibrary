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
            this.AddTestAssembly(typeof(DispatcherServiceTests).Assembly);
        }
    }
}