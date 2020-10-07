using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Android.Services;
using SampleApp.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SampleApp.Droid
{
    [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.FontScale)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            HyperLinkLabelRenderer.Init();
            Forms.Init(this, savedInstanceState);

            // Use custom IFontConverter implementation in order to scale all font sizes
            //global::CrossPlatformLibrary.Forms.CrossPlatformLibrary.SetFontConverter(new SampleApp.Droid.Services.CustomFontSizeConverter());

            var activityIndicatorService = new AndroidActivityIndicatorService(() => Xamarin.Essentials.Platform.CurrentActivity);
            activityIndicatorService.Init(new SampleActivityIndicatorPage());
            this.LoadApplication(new App(activityIndicatorService));
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}