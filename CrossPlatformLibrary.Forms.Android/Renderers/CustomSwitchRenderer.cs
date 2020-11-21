using System;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Color = Android.Graphics.Color;
using Switch = Android.Widget.Switch;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class CustomSwitchRenderer : ViewRenderer<CustomSwitch, Switch>
    {
        public CustomSwitchRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<CustomSwitch> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null)
            {
                e.OldElement.Toggled -= this.ElementToggled;
            }

            if (e.NewElement != null)
            {
                var sw = new Switch(this.Context);

                this.SetNativeControl(sw);
                this.Control.Checked = e.NewElement.IsToggled;
                this.Control.CheckedChange += this.ControlValueChanged;
                this.SetTintColor(this.Element.TintColor.ToAndroid());
                this.Element.Toggled += this.ElementToggled;
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (e.PropertyName == Xamarin.Forms.Switch.IsToggledProperty.PropertyName)
            {
                this.SetTintColor(this.Element.TintColor.ToAndroid());
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.Control.CheckedChange -= this.ControlValueChanged;
                this.Element.Toggled -= this.ElementToggled;
            }

            base.Dispose(disposing);
        }

        private void SetTintColor(Color color)
        {
            if (this.Control == null || this.Element == null)
            {
                return;
            }

            if (this.Control.Checked)
            {
                this.Control.ThumbDrawable.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
                this.Control.TrackDrawable.SetColorFilter(color, PorterDuff.Mode.SrcAtop);
            }
            else
            {
                this.Control.ThumbDrawable.SetColorFilter(null);
                this.Control.TrackDrawable.SetColorFilter(null);
            }
        }

        private void ElementToggled(object sender, ToggledEventArgs e)
        {
            this.Control.Checked = this.Element.IsToggled;
            this.SetTintColor(this.Element.TintColor.ToAndroid());
        }

        private void ControlValueChanged(object sender, EventArgs e)
        {
            this.Element.IsToggled = this.Control.Checked;
            this.SetTintColor(this.Element.TintColor.ToAndroid());
        }
    }
}