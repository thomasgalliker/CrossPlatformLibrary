using System;
using System.ComponentModel;
using System.Diagnostics;
using Android.Widget;
using CrossPlatformLibrary.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using EntryLineColorEffect = CrossPlatformLibrary.Forms.Android.Effects.EntryLineColorEffect;

[assembly: ExportEffect(typeof(EntryLineColorEffect), nameof(EntryLineColorEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class EntryLineColorEffect : PlatformEffect
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
                Debug.WriteLine(ex.Message);
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
                if (this.control != null)
                {
                    var lineColor = LineColorEffect.GetLineColor(this.Element);
                    this.control.Background.SetColorFilter(lineColor.ToAndroid(), global::Android.Graphics.PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}