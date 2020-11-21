using System;
using System.ComponentModel;
using CrossPlatformLibrary.Internals;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using FormsTintImageEffect = CrossPlatformLibrary.Forms.Effects.TintImageEffect;
using TintImageEffect = CrossPlatformLibrary.Forms.Android.Effects.TintImageEffect;

[assembly: ExportEffect(typeof(TintImageEffect), nameof(TintImageEffect))]
namespace CrossPlatformLibrary.Forms.Android.Effects
{
    public class TintImageEffect : PlatformEffect
    {
        private UIImageView control;

        protected override void OnAttached()
        {
            this.control = this.Control as UIImageView;
            this.UpdateTintColor();
        }

        protected override void OnElementPropertyChanged(PropertyChangedEventArgs args)
        {
            if (args.PropertyName == FormsTintImageEffect.TintColorProperty.PropertyName)
            {
                this.UpdateTintColor();
            }
            else if (args.PropertyName == Image.IsLoadingProperty.PropertyName)
            {
                this.UpdateTintColor();
            }
        }

        private void UpdateTintColor()
        {
            try
            {
                if (this.control.Image is UIImage uiImage)
                {
                    var tintColor = FormsTintImageEffect.GetTintColor(this.Element);
                    this.control.Image = uiImage.ImageWithRenderingMode(UIImageRenderingMode.AlwaysTemplate);
                    this.control.TintColor = tintColor.ToUIColor();
                }
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
