using System;
using System.ComponentModel;
using System.Diagnostics;
using CrossPlatformLibrary.Forms.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using EditorLineColorEffect = CrossPlatformLibrary.Forms.Android.Effects.EditorLineColorEffect;

[assembly: ExportEffect(typeof(EditorLineColorEffect), nameof(EditorLineColorEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class EditorLineColorEffect : PlatformEffect
    {
        FormsEditText control;

        protected override void OnAttached()
        {
            try
            {
                this.control = this.Control as FormsEditText;

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