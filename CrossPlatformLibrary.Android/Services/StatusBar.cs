using System;
using System.Threading;
using Android.Graphics;
using Android.Views;

namespace CrossPlatformLibrary.Services
{
    public class StatusBar : IStatusBar
    {
        private static readonly Lazy<IStatusBar> Implementation = new Lazy<IStatusBar>(CreateLocalizer, LazyThreadSafetyMode.PublicationOnly);

        public static IStatusBar Current => Implementation.Value;

        private static IStatusBar CreateLocalizer()
        {
            return new StatusBar();
        }

        public string GetColor()
        {
            var window = CrossPlatformLibrary.CurrentActivity.Window;
            return new Color(window.StatusBarColor).ToString();
        }

        public void SetColor(string hexColor)
        {
            var window = CrossPlatformLibrary.CurrentActivity.Window;
            var color = Color.ParseColor(hexColor);
            this.SetStatusBarColor(window, color);
        }

        private void SetStatusBarColor(Window window, Color color)
        {
            window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
            window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
            window.SetStatusBarColor(color);
        }
    }
}