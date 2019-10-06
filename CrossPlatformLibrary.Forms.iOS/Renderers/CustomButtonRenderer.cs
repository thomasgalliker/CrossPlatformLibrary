using System;
using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Extensions;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(Button), typeof(CrossPlatformLibrary.Forms.iOS.Renderers.ButtonRenderer))]
[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    /// <summary>
    /// ButtonRenderer: Implements a workaround for missing Released event if button was released outside the visible area (MotionEventActions.Cancel).
    /// See here: https://github.com/xamarin/Xamarin.Forms/issues/3523
    /// </summary>
    public class ButtonRenderer : Xamarin.Forms.Platform.iOS.ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (this.Control != null && e.NewElement is Button button)
            {
                this.Control.TouchCancel += this.SendReleased;
                this.Control.TouchDragExit += this.SendReleased;
                this.Control.TouchDragEnter += this.SendPressed;
            }
        }

        private void SendReleased(object sender, EventArgs e)
        {
            ((IButtonController)this.Element)?.SendReleased();
        }

        private void SendPressed(object sender, EventArgs e)
        {
            ((IButtonController)this.Element)?.SendPressed();
        }

        protected override void Dispose(bool disposing)
        {
            if (this.Control != null)
            {
                this.Control.TouchDragExit -= this.SendReleased;
                this.Control.TouchCancel -= this.SendReleased;
                this.Control.TouchDragEnter -= this.SendPressed;
            }

            base.Dispose(disposing);
        }
    }

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

            if (e.PropertyName == CustomButton.VerticalContentAlignmentProperty.PropertyName)
            {
                if (this.Element is CustomButton customButton)
                {
                    this.UpdateVerticalAlignment(customButton);
                }
            }
            else if (e.PropertyName == CustomButton.HorizontalContentAlignmentProperty.PropertyName)
            {
                if (this.Element is CustomButton customButton)
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