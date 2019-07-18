using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomLabel), typeof(CustomLabelRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomLabelRenderer : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);

            var customLabel = (CustomLabel)this.Element;

            if (customLabel != null && customLabel.Lines != -1)
            {
                this.Control.Lines = customLabel.Lines;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            // TODO: Handle Lines property changed
        }
    }
}