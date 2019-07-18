using Android.Content;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomLabelRenderer : Xamarin.Forms.Platform.Android.LabelRenderer
    {
        public CustomLabelRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var multiLineLabel = (CustomLabel)this.Element;
            if (multiLineLabel != null && multiLineLabel.Lines != -1)
            {
                this.Control.SetSingleLine(false);
                this.Control.SetLines(multiLineLabel.Lines);
            }
        }
    }
}