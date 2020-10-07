using CrossPlatformLibrary.Forms.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SampleApp.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SampleActivityIndicatorPage : ContentPage, IActivityIndicatorPage
    {
        public SampleActivityIndicatorPage()
        {
            this.InitializeComponent();
        }

        public void SetCaption(string text)
        {
            this.Label.Text = text;
        }
    }
}