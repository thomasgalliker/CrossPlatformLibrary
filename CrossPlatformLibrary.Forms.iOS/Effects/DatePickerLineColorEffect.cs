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
using DatePickerLineColorEffect = CrossPlatformLibrary.Forms.iOS.Effects.DatePickerLineColorEffect;

[assembly: ExportEffect(typeof(DatePickerLineColorEffect), nameof(DatePickerLineColorEffect))]
namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    public class DatePickerLineColorEffect : PlatformEffect
    {
        UITextField control;

        protected override void OnAttached()
        {
            try
            {
                this.control = this.Control as UITextField;
                this.Initialize();
                this.UpdateLineColor();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }

        protected override void OnDetached()
        {
            this.control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            base.OnElementPropertyChanged(args);

            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName ||
                args.PropertyName == "Height")
            {
                this.Initialize();
                this.UpdateLineColor();
            }
        }

        private void Initialize()
        {
            if (this.Element is DatePicker datePicker)
            {
                this.Control.Bounds = new CGRect(0, 0, datePicker.Width, datePicker.Height);
            }
        }

        private void UpdateLineColor()
        {
            if (this.control == null)
                return;

            var lineLayer = this.control.Layer.Sublayers
               .OfType<BorderLineLayer>()
               .SingleOrDefault();

            if (lineLayer == null)
            {
                lineLayer = new BorderLineLayer
                {
                    MasksToBounds = true, 
                    BorderWidth = 1.0f
                };
                this.control.Layer.AddSublayer(lineLayer);
                this.control.BorderStyle = UITextBorderStyle.None;
            }

            var lineY = this.Control.Frame.Height * 0.9;
            lineLayer.Frame = new CGRect(0f, lineY, this.Control.Bounds.Width, 1f);
            Debug.WriteLine($"DatePickerLineColorEffect: Control.Frame: H:{this.Control.Bounds.Height} W:{this.Control.Bounds.Width} --> lineLayer.Frame: Y:{lineLayer.Frame.Y}");
            lineLayer.BorderColor = LineColorEffect.GetLineColor(this.Element).ToCGColor();
            this.control.TintColor = this.control.TextColor;
        }

        private class BorderLineLayer : CALayer
        {
        }
    }
}