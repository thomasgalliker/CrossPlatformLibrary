using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
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
            this.UpdateLines(customLabel);
            this.JustifyText(customLabel);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Element is CustomLabel customLabel)
            {
                if (e.PropertyName == CustomLabel.LinesProperty.PropertyName)
                {
                    this.UpdateLines(customLabel);
                }
                else if (e.PropertyName == CustomLabel.JustifyTextProperty.PropertyName)
                {
                    this.JustifyText(customLabel);
                }
            }
        }

        private void JustifyText(CustomLabel customLabel)
        {
            if (customLabel.JustifyText)
            {
                this.Control.TextAlignment = UITextAlignment.Justified;
            }
            else
            {
                this.Control.TextAlignment = UITextAlignment.Left;
            }
        }

        private void UpdateLines(CustomLabel customLabel)
        {
            if (customLabel.Lines != CustomLabel.DefaultLinesValue)
            {
                this.Control.Lines = customLabel.Lines;
            }
            else
            {
                // TODO How to switch back?
            }
        }
    }
}