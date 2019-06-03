using Xamarin.Forms;

namespace ResourceDictionaryDemo
{
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