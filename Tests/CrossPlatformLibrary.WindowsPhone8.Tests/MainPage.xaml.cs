using System.Reflection;

using CrossPlatformLibrary.Tests.IO;

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
            // tests can be inside the main assembly
            this.AddTestAssembly(Assembly.GetExecutingAssembly());
            this.AddTestAssembly(typeof(XmlSerializerHelperTests).Assembly);

            // otherwise you need to ensure that the test assemblies will 
            // become part of the app bundle
            ////this.AddTestAssembly(typeof(PortableTests).Assembly);
        }
    }
}


