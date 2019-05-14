using CrossPlatformLibrary.Forms.Localization;
using CrossPlatformLibrary.Localization;
using SampleApp.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]

namespace SampleApp
{
    public partial class App : Application
    {
        public App()
        {



            // Initialize localization
            ILocalizer localizer = Localizer.Current;
            var translationProvider = ResxSingleTranslationProvider.Instance;
            translationProvider.Init(Strings.ResourceManager);
            TranslateExtension.Init(localizer, translationProvider);

            this.InitializeComponent();

            // Initialize CrossPlatformLibrary.Forms
            CrossPlatformLibrary.Forms.CrossPlatformLibrary.Init(this);

            this.MainPage = new MainPage();
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