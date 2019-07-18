using System;
using System.ComponentModel;
using Android.Widget;
using CrossPlatformLibrary.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using TimePickerLineColorEffect = CrossPlatformLibrary.Forms.Android.Effects.TimePickerLineColorEffect;

[assembly: ExportEffect(typeof(TimePickerLineColorEffect), nameof(TimePickerLineColorEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class TimePickerLineColorEffect : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            this.control = this.Control as EditText;
            this.UpdateLineColor();
        }

        protected override void OnDetached()
        {
            this.control = null;
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == LineColorEffect.LineColorProperty.PropertyName)
            {
                this.UpdateLineColor();
            }
        }

        private void UpdateLineColor()
        {
            try
            {
                var lineColor = LineColorEffect.GetLineColor(this.Element);
                this.control.Background.SetColorFilter(lineColor.ToAndroid(), global::Android.Graphics.PorterDuff.Mode.SrcAtop);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Cannot set property on attached control. Error: ", ex.Message);
            }
        }
    }
}