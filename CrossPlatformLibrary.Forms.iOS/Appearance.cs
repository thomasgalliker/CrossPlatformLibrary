using CrossPlatformLibrary.Forms.Themes;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CrossPlatformLibrary.Forms.iOS
{
    public static class Appearance
    {
        /// <summary>
        /// Sets theme colors from current application's resources to iOS UI elements.
        /// </summary>
        /// <param name="app">The current application.</param>
        public static void Configure(Application app)
        {
            if (app.Resources.TryGetValue(ThemeConstants.Color.Primary, out var primaryColorResource))
            {
                if(primaryColorResource is Color primaryColor)
                {
                    global::CrossPlatformLibrary.iOS.Appearance.SetTintColor(primaryColor.ToUIColor());
                }
            }
        }
    }
}