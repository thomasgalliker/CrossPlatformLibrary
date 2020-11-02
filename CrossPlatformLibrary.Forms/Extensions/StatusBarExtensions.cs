using System;
using CrossPlatformLibrary.Services;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Extensions
{
    public static class StatusBarExtensions
    {
        public static void SetColor(this IStatusBar statusBar, Color color)
        {
            statusBar.SetColor(color.ToHex());
        }
    }
}