using Android.Graphics;
using Android.Views;

namespace CrossPlatformLibrary.Services
{
    public class StatusBar : IStatusBar
    {
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