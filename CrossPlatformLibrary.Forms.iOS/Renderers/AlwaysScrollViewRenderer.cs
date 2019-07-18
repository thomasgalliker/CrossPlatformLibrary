using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(AlwaysScrollView), typeof(AlwaysScrollViewRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class AlwaysScrollViewRenderer : ScrollViewRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            //if (e.OldElement != null)
            //{
            //    // Unsubscribe
            //}

            if (e.NewElement != null)
            {
                this.AlwaysBounceVertical = true;
            }
        }
    }
}