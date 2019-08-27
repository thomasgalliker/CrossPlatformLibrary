using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CustomSwitch), typeof(CustomSwitchRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class CustomSwitchRenderer : SwitchRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Switch> e)
        {
            base.OnElementChanged(e);

            if (this.Element is CustomSwitch customSwitch)
            {
                this.UpdateTintColor(customSwitch);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == CustomSwitch.TintColorProperty.PropertyName)
            {
                var customSwitch = (CustomSwitch)this.Element;
                this.UpdateTintColor(customSwitch);
            }
        }

        private void UpdateTintColor(CustomSwitch customSwitch)
        {
            if (this.Control != null)
            {
                this.Control.OnTintColor = customSwitch.TintColor.ToUIColor();
            }
        }
    }
}