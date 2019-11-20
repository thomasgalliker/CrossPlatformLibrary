using System;
using System.Diagnostics;
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

        private void AddressControl_OnSizeChanged(object sender, EventArgs e)
        {
            var x = (AddressControl)sender;
            Debug.WriteLine($"AddressControl_OnSizeChanged: H:{x.Height}, W:{x.Width}");
        }

        private void AddressControl_OnLayoutChanged(object sender, EventArgs e)
        {
            var x = (AddressControl)sender;
            Debug.WriteLine($"AddressControl_OnLayoutChanged: H:{x.Height}, W:{x.Width}");
        }
    }
}
