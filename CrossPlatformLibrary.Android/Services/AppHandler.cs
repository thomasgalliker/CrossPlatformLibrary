using System;
using Android.App;
using Android.Content;
using Uri = Android.Net.Uri;

namespace CrossPlatformLibrary.Services
{
    public class AppHandler : IAppHandler
    {
        public bool LaunchApp(string uri)
        {
            try
            {
                var aUri = Uri.Parse(uri);
                var intent = new Intent(Intent.ActionView, aUri);
                Application.Context.StartActivity(intent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpenLocationServiceSettings()
        {
            try
            {
                var intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                Application.Context.StartActivity(intent);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpenAppSettings()
        {
            try
            {
                var context = Application.Context;

                var settingsIntent = new Intent();
                settingsIntent.SetAction(Android.Provider.Settings.ActionApplicationDetailsSettings);
                settingsIntent.AddCategory(Intent.CategoryDefault);
                settingsIntent.SetData(Uri.Parse("package:" + context.PackageName));
                settingsIntent.AddFlags(ActivityFlags.NewTask);
                settingsIntent.AddFlags(ActivityFlags.NoHistory);
                settingsIntent.AddFlags(ActivityFlags.ExcludeFromRecents);
                context.StartActivity(settingsIntent);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}