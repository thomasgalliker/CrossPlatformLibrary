using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }
    }
}