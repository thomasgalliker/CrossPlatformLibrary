using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TabbedMainPage : CustomTabbedPage
    {
        public TabbedMainPage()
        {
            this.InitializeComponent();
        }
    }
}