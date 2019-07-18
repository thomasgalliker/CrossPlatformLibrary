using Android.App;
using Android.Content.PM;
using Android.OS;
using CrossPlatformLibrary.Forms.Android.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace SampleApp.Droid
{
    [Activity(Label = "SampleApp", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;

            base.OnCreate(savedInstanceState);
            HyperLinkLabelRenderer.Init();
            Forms.Init(this, savedInstanceState);
            this.LoadApplication(new App());
        }
    }
}