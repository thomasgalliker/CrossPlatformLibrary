using System;
using System.Threading;
using Android.Graphics;
using Android.OS;
using Android.Views;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Services
{
    public class StatusBarService : IStatusBarService
    {
        private static readonly Lazy<IStatusBarService> Implementation = new Lazy<IStatusBarService>(CreateStatusBar, LazyThreadSafetyMode.PublicationOnly);

        public static IStatusBarService Current => Implementation.Value;

        private static IStatusBarService CreateStatusBar()
        {
            return new StatusBarService();
        }

        public void SetHexColor(string hexColor)
        {
            var window = CrossPlatformLibrary.CurrentActivity.Window;
            var color = hexColor.ToColor();
            this.SetStatusBarColor(window, color);
        }

        private void SetStatusBarColor(Window window, Color color)
        {
            window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color);
        }

        public void SetStatusBarMode(StatusBarStyle statusBarStyle)
        {
            var window = CrossPlatformLibrary.CurrentActivity.Window;
            var windowLightStatusBar = statusBarStyle == StatusBarStyle.Light;

            if (Build.VERSION.SdkInt >= BuildVersionCodes.M)
            {
                var newUiVisibility = (int)window.DecorView.SystemUiVisibility;
                if (windowLightStatusBar)
                {
                    newUiVisibility |= (int)SystemUiFlags.LightStatusBar;
                }
                else
                {
                    newUiVisibility &= ~(int)SystemUiFlags.LightStatusBar;
                }

                window.DecorView.SystemUiVisibility = (StatusBarVisibility)newUiVisibility;
            }
        }
    }
}