using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.BindingContext = new MainViewModel();
        }
    }
}