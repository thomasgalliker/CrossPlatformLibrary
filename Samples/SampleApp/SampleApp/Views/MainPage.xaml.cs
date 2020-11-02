using System;
using System.Threading.Tasks;
using CrossPlatformLibrary.Forms.Extensions;
using CrossPlatformLibrary.Forms.Services;
using CrossPlatformLibrary.Services;
using SampleApp.Services;
using SampleApp.Validation;
using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage(IActivityIndicatorService activityIndicatorService, IStatusBar statusBar)
        {
            try
            {
                this.InitializeComponent();
                statusBar.SetColor(Color.Magenta);

                var displayService = new DisplayService((t, m) => this.DisplayAlert(t, m, "OK"));
                var countryService = new CountryServiceMock();
                var validationService = new ValidationService();
                var emailService = new EmailService();
                var navigationService = new NavigationService(this);
                this.BindingContext = new MainViewModel(navigationService, displayService, countryService, validationService, emailService, activityIndicatorService);
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

    public class NavigationService : INavigationService
    {
        private readonly Page currentPage;

        public NavigationService(Page page)
        {
            this.currentPage = page;
        }

        public Task PushAsync(Page page)
        {
            return this.currentPage.Navigation.PushAsync(page);
        }
    }

    public interface INavigationService
    {
        Task PushAsync(Page page);
    }
}