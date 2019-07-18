using System;
using Foundation;
using UIKit;

namespace CrossPlatformLibrary.Services
{
    public class AppHandler : IAppHandler
    {
        public bool LaunchApp(string uri)
        {
            var nsUrl = new NSUrl(uri);
            var canOpen = UIApplication.SharedApplication.CanOpenUrl(nsUrl);

            if (!canOpen)
            {
                return false;
            }

            return UIApplication.SharedApplication.OpenUrl(nsUrl);
        }

        public bool OpenAppSettings()
        {
            try
            {
                this.LaunchApp(UIApplication.OpenSettingsUrlString);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool OpenLocationServiceSettings()
        {
            return false;
        }
    }
}