using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(NonScrollableListView), typeof(NonScrollableListViewRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class NonScrollableListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.ScrollEnabled = false;
                }
            }
        }
    }
}

