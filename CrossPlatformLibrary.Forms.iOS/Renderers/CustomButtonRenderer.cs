using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Extensions;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    /// <summary>
    ///     Source: http://www.wintellect.com/devcenter/jprosise/supercharging-xamarin-forms-with-custom-renderers-part-1
    /// </summary>
    public class CustomButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (this.Element is CustomButton customButton)
            {
                var button = this.Control;
                if (button != null)
                {
                    button.LineBreakMode = UILineBreakMode.TailTruncation;

                    this.UpdateHorizontalAlignment(customButton);
                    this.UpdateVerticalAlignment(customButton);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var customButton = this.Element as CustomButton;
            if (customButton != null)
            {
                if (e.PropertyName == CustomButton.VerticalContentAlignmentProperty.PropertyName)
                {
                    this.UpdateVerticalAlignment(customButton);
                }
                else if (e.PropertyName == CustomButton.HorizontalContentAlignmentProperty.PropertyName)
                {
                    this.UpdateHorizontalAlignment(customButton);
                }
            }
        }

        private void UpdateHorizontalAlignment(CustomButton customButton)
        {
            this.Control.HorizontalAlignment = customButton.HorizontalContentAlignment.ToContentHorizontalAlignment();
        }

        private void UpdateVerticalAlignment(CustomButton customButton)
        {
            this.Control.VerticalAlignment = customButton.VerticalContentAlignment.ToContentVerticalAlignment();
        }
    }
}