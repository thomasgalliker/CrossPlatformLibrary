using System.ComponentModel;
using Android.Content;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(NoBarsScrollViewer), typeof(NoBarsScrollViewerRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class NoBarsScrollViewerRenderer : ScrollViewRenderer
    {
        public NoBarsScrollViewerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
            {
                return;
            }

            if (e.OldElement != null)
            {
                e.OldElement.PropertyChanged -= this.OnElementPropertyChanged;
            }

            e.NewElement.PropertyChanged += this.OnElementPropertyChanged;
        }

        private void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == ScrollView.ContentSizeProperty.PropertyName && this.ChildCount > 0)
            {
                var child = this.GetChildAt(0);
                child.VerticalScrollBarEnabled = false;
                child.HorizontalScrollBarEnabled = false;
            }
        }
    }
}