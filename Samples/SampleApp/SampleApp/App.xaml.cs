using System;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.Localization;
using CrossPlatformLibrary.Localization;
using SampleApp.Resources;
using SampleApp.Views;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SampleApp
{
    public partial class App : Application
    {
        public static readonly TimeSpan SearchCommandDelay = TimeSpan.FromMilliseconds(500);

        public App()
        {
            // Initialize localization
            ILocalizer localizer = Localizer.Current;
            var translationProvider = ResxSingleTranslationProvider.Current;
            translationProvider.Init(Strings.ResourceManager);
            TranslateExtension.Init(localizer, translationProvider);
            ImageResourceExtension.Init(typeof(App).Assembly);

            this.InitializeComponent();

            // Initialize CrossPlatformLibrary.Forms
            CrossPlatformLibrary.Forms.CrossPlatformLibrary.Init(this, "SampleApp.Theme");

            this.MainPage = new NavigationPage(new MainPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}