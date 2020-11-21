using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CustomActivityIndicatorPage : ContentPage, IActivityIndicatorPage
    {
        public CustomActivityIndicatorPage()
        {
            this.InitializeComponent();
        }

        public void SetCaption(string text)
        {
            this.ActivityIndicator.Caption = text;
        }
    }
}