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

            //this["CustomButtonPrimaryStyle.TextColor"] = this["WindowBackground"];
            //this["CustomButtonPrimaryStyle.BorderColor.Enabled"] = this["Primary"];
            //this["CustomButtonPrimaryStyle.BorderColor.Pressed"] = Color.Black;
            //this["CustomButtonPrimaryStyle.BackgroundColor.Enabled"] = this["Primary"];
            //this["CustomButtonPrimaryStyle.BackgroundColor.Pressed"] = this["Primary"];

            //this["CustomButtonSecondaryStyle.TextColor"] = this["Primary"];
            //this["CustomButtonSecondaryStyle.BorderColor.Enabled"] = this["Primary"];
            //this["CustomButtonSecondaryStyle.BorderColor.Pressed"] = this["Primary"];
            //this["CustomButtonSecondaryStyle.BackgroundColor.Enabled"] = this["WindowBackground"];
            //this["CustomButtonSecondaryStyle.BackgroundColor.Pressed"] = this["Primary"];
        }
    }
}