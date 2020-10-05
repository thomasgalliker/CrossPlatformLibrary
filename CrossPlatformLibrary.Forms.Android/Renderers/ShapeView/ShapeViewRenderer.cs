using System.ComponentModel;
using Android.Content;
using CrossPlatformLibrary.Forms.Android.Renderers.ShapeView;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CrossPlatformLibrary.Forms.Controls.ShapeView), typeof(ShapeViewRenderer))]
namespace CrossPlatformLibrary.Forms.Android.Renderers.ShapeView
{
    public class ShapeViewRenderer : ViewRenderer<Controls.ShapeView, Shape>
    {
        public ShapeViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Controls.ShapeView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null || this.Element == null)
                return;

            this.SetNativeControl(new Shape(this.Resources.DisplayMetrics.Density, this.Context)
            {
                ShapeView = this.Element
            });
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == BoxView.ColorProperty.PropertyName || e.PropertyName == Controls.ShapeView.StrokeColorProperty.PropertyName)
            {
                this.Control.Invalidate();
            }
        }
    }
}
