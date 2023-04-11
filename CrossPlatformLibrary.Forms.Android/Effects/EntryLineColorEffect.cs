using System;
using System.ComponentModel;
using Android.Graphics;
using Android.Widget;
using CrossPlatformLibrary.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Debug = System.Diagnostics.Debug;
using EntryLineColorEffect = CrossPlatformLibrary.Forms.Android.Effects.EntryLineColorEffect;

[assembly: ExportEffect(typeof(EntryLineColorEffect), nameof(EntryLineColorEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class EntryLineColorEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                this.UpdateLineColor();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        protected override void OnDetached()
        {
            // Nothing to do here
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
                if (this.Control is EditText editText)
                {
                    var lineColor = LineColorEffect.GetLineColor(this.Element);
                    editText.Background.SetColorFilter(lineColor.ToAndroid(), PorterDuff.Mode.SrcAtop);
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}