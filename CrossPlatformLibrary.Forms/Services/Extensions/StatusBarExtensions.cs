using System;
using CrossPlatformLibrary.Services;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Services
{
    public static class StatusBarExtensions
    {
        public static void SetColor(this IStatusBarService statusBar, Color color)
        {
            statusBar.SetHexColor(color.ToHex());
        }
    }
}