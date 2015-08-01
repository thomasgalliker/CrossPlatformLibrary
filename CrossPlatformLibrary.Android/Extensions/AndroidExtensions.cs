
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Runtime;
using Android.Util;
using Android.Views;


namespace CrossPlatformLibrary.Droid.Extensions
{
    public static class AndroidExtensions
    {
        private static float density;

        public static void Initialize(Context context)
        {
            var wm = context.GetSystemService(Context.WindowService).JavaCast<IWindowManager>();
            var displayMetrics = new DisplayMetrics();
            wm.DefaultDisplay.GetMetrics(displayMetrics);
            density = displayMetrics.Density;

            var bg = new TypedValue();
            context.Theme.ResolveAttribute(Android.Resource.Attribute.ColorBackground, bg, true);
            DefaultBackground = new ColorDrawable(new Color(bg.Data));
        }

        public static int ToPixels(this int dp)
        {
            return (int)(dp * density + 0.5f);
        }

        public static ColorDrawable DefaultBackground { get; private set; }
    }
}