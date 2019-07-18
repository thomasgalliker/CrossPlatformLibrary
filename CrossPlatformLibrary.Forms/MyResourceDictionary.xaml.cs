using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyResourceDictionary : ResourceDictionary
    {
        public MyResourceDictionary()
        {
            this.InitializeComponent();

            this["CustomButtonStyle.TextColor"] = this["Primary"];

            this["CustomButtonPrimaryStyle.BackgroundColor.Enabled"] = this["Primary"];
            this["CustomButtonPrimaryStyle.BorderColor.Enabled"] = this["Primary"];

            this["CustomButtonSecondaryStyle.BorderColor.Enabled"] = this["Primary"];
        }
    }
}