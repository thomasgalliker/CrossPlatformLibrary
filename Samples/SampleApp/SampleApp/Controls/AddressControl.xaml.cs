using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddressControl
    {
        public AddressControl()
        {
            this.InitializeComponent();
        }

        private void ValidatableEntry_OnUnfocused(object sender, FocusEventArgs e)
        {
        }
    }
}
