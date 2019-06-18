using Android.Content;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NonScrollableListView), typeof(NonScrollableListViewRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class NonScrollableListViewRenderer : ListViewRenderer
    {
        public NonScrollableListViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                if (this.Control != null)
                {
                    this.Control.VerticalScrollBarEnabled = false;
                    this.Control.HorizontalScrollBarEnabled = false;
                }
            }
        }
    }
}

