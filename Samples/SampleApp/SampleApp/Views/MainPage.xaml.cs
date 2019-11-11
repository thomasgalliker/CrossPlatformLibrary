using System;
using System.Threading.Tasks;
using CrossPlatformLibrary.Forms.Themes;
using SampleApp.Services;
using SampleApp.Validation;
using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            try
            {
                this.InitializeComponent();

                var displayService = new DisplayService((t, m) => this.DisplayAlert(t, m, "OK"));
                var countryService = new CountryServiceMock();
                var validationService = new ValidationService();
                this.BindingContext = new MainViewModel(displayService, countryService, validationService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void AutoCompleteView_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            this.ScrollView.ScrollToAsync(this.AutoCompleteView, ScrollToPosition.Start, animated: true);
        }
    }

    public class DisplayService : IDisplayService
    {
        private readonly Func<string, string, Task> alertAction;

        public DisplayService(Func<string, string, Task> alertAction)
        {
            this.alertAction = alertAction;
        }
        public async Task DisplayAlert(string title, string message)
        {
            await this.alertAction(title, message);
        }
    }
    public interface IDisplayService
    {
        Task DisplayAlert(string title, string message);
    }
}