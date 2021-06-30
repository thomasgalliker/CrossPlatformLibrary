
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.Xaml;

namespace SampleApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EntryPage : ContentPage
    {
        private WindowSoftInputModeAdjust originalWindowSoftInputModeAdjust;

        public EntryPage()
        {
            this.InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            var platformElementConfiguration = App.Current.On<Android>();
            this.originalWindowSoftInputModeAdjust = platformElementConfiguration.GetWindowSoftInputModeAdjust();
            platformElementConfiguration.UseWindowSoftInputModeAdjust(WindowSoftInputModeAdjust.Resize);
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            var platformElementConfiguration = App.Current.On<Android>();
            platformElementConfiguration.UseWindowSoftInputModeAdjust(this.originalWindowSoftInputModeAdjust);
        }
    }
}