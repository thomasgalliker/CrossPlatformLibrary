using System.ComponentModel;
using Android.Graphics;
using Android.Widget;
using CrossPlatformLibrary.Internals;
using Java.Lang;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using FormsTintImageEffect = CrossPlatformLibrary.Forms.Effects.TintImageEffect;
using TintImageEffect = CrossPlatformLibrary.Forms.Android.Effects.TintImageEffect;

[assembly: ExportEffect(typeof(TintImageEffect), nameof(TintImageEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class TintImageEffect : PlatformEffect
    {
        private ImageView control;

        protected override void OnAttached()
        {
            this.control = this.Control as ImageView;
            this.UpdateTintColor();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == FormsTintImageEffect.TintColorProperty.PropertyName)
            {
                this.UpdateTintColor();
            }
        }

        private void UpdateTintColor()
        {
            try
            {
                var tintColor = FormsTintImageEffect.GetTintColor(this.Element);
                var filter = new PorterDuffColorFilter(tintColor.ToAndroid(), PorterDuff.Mode.SrcIn);
                this.control.SetColorFilter(filter);
            }
            catch (Exception ex)
            {
                Tracer.Current.Exception(ex, $"UpdateTintColor failed with exception");
            }
        }
        protected override void OnDetached()
        {
            this.control = null;
        }
    }
}
