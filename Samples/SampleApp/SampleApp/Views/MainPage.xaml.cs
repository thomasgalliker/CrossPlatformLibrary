using System;
using System.Threading.Tasks;
using CrossPlatformLibrary.Forms.Extensions;
using CrossPlatformLibrary.Forms.Services;
using CrossPlatformLibrary.Localization;
using CrossPlatformLibrary.Services;
using SampleApp.Services;
using SampleApp.Validation;
using SampleApp.ViewModels;
using Xamarin.Forms;

namespace SampleApp.Views
{
    public partial class MainPage : ContentPage
    {
        private readonly IStatusBarService statusBar;

        public MainPage(ILocalizer localizer, IActivityIndicatorService activityIndicatorService, IStatusBarService statusBar)
        {
            try
            {
                this.InitializeComponent();

                var displayService = new DisplayService((t, m) => this.DisplayAlert(t, m, "OK"));
                var countryService = new CountryServiceMock();
                var validationService = new ValidationService();
                var emailService = new EmailService();
                var navigationService = new NavigationService(this);
                this.BindingContext = new MainViewModel(navigationService, displayService, countryService, validationService, emailService, localizer, activityIndicatorService);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                 throw;
            }

            this.statusBar = statusBar;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            statusBar.SetColor(Color.Black);
            statusBar.SetStatusBarMode(StatusBarStyle.Dark);
        }

        protected override void OnDisappearing()
        {
            base.OnAppearing();

            statusBar.SetColor(Color.White);
            statusBar.SetStatusBarMode(StatusBarStyle.Light);
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