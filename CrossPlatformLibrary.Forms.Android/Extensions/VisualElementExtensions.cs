using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

namespace CrossPlatformLibrary.Forms.Android.Extensions
{
    public static class VisualElementExtensions
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable, Context context)
        {
            var renderer = Platform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = Platform.CreateRendererWithContext(bindable, context);
                Platform.SetRenderer(bindable, renderer);
            }

            return renderer;
        }
    }
}