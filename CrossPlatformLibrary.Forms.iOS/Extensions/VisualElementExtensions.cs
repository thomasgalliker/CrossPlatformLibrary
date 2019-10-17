using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CrossPlatformLibrary.Forms.iOS.Extensions
{
    public static class VisualElementExtensions
    {
        public static IVisualElementRenderer GetOrCreateRenderer(this VisualElement bindable)
        {
            var renderer = Platform.GetRenderer(bindable);
            if (renderer == null)
            {
                renderer = Platform.CreateRenderer(bindable);
                Platform.SetRenderer(bindable, renderer);
            }

            return renderer;
        }
    }
}