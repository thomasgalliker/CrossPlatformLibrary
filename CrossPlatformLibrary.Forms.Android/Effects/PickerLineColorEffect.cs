using System;
using System.ComponentModel;
using System.Diagnostics;
using Android.Widget;
using CrossPlatformLibrary.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using PickerLineColorEffect = CrossPlatformLibrary.Forms.Android.Effects.PickerLineColorEffect;

[assembly: ExportEffect(typeof(PickerLineColorEffect), "PickerLineColorEffect")]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class PickerLineColorEffect : PlatformEffect
    {
        EditText control;

        protected override void OnAttached()
        {
            try
            {
                this.control = this.Control as EditText;
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
                Debug.WriteLine(ex.Message);
            }
        }
    }
}