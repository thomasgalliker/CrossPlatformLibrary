using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using CoreAnimation;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Effects;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public abstract class LineColorEffectBase : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                this.UpdateLineColor();
            }
            catch (Exception ex)
            {
                this.Log($"Cannot set property on attached control. Error: {ex.Message}");
            }
        }

        protected override void OnDetached()
        {
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName ||
                args.PropertyName == "Height")
            {
                this.UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            if (!(this.Control is UITextField control))
            {
                return;
            }

            var lineLayer = control.Layer.Sublayers
                .OfType<BorderLineLayer>()
                .SingleOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new BorderLineLayer
                {
                    MasksToBounds = true,
                    BorderWidth = 1.0f
                };
                control.Layer.AddSublayer(lineLayer);
                control.BorderStyle = UITextBorderStyle.None;
            }

            if (this.Element is VisualElement visualElement && visualElement.Height > 0)
            {
                var lineY = Math.Min(visualElement.Height - 10, visualElement.Height * 0.87);
                lineLayer.Frame = new CGRect(0f, lineY, visualElement.Width, 1f);
                this.Log($"visualElement: H:{visualElement.Height} W:{visualElement.Width} --> lineLayer.Frame: Y:{lineLayer.Frame.Y}");
                lineLayer.BorderColor = LineColorEffect.GetLineColor(this.Element).ToCGColor();
                control.TintColor = control.TextColor;
            }
        }

        [Conditional("DEBUG")]
        private void Log(string message)
        {
            Debug.WriteLine($"{this.GetType().Name}: {message}");
        }

        private class BorderLineLayer : CALayer
        {
        }
    }
}