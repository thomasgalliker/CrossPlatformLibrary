
using Android.App;
using Android.OS;

using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.IntegrationTests.Dispatching;

using Xunit.Runners.UI;
using Xunit.Sdk;

namespace IntegrationTests.Android
{
    [Activity(Label = "IntegrationTests.Android", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : RunnerActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            var bootstrapper = new Bootstrapper();
            bootstrapper.Startup();

            this.AddTestAssembly(typeof(DispatcherServiceTests).Assembly);

            this.AddExecutionAssembly(typeof(ExtensibilityPointFactory).Assembly);
            // or in any reference assemblies			

#if false
    // you can use the default or set your own custom writer (e.g. save to web site and tweet it ;-)
			Writer = new TcpTextWriter ("10.0.1.2", 16384);
			// start running the test suites as soon as the application is loaded
			AutoStart = true;
			// crash the application (to ensure it's ended) and return to springboard
			TerminateAfterExecution = true;
#endif
            // you cannot add more assemblies once calling base
            base.OnCreate(bundle);
        }
    }
}