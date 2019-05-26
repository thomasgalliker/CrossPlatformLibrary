using System.ComponentModel;
using Android.Content;
using Android.Graphics.Drawables;
using CrossPlatformLibrary.Forms.Android.Extensions;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomButton), typeof(CustomButtonRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    /// <summary>
    ///     Source: http://www.wintellect.com/devcenter/jprosise/supercharging-xamarin-forms-with-custom-renderers-part-1
    /// </summary>
    public class CustomButtonRenderer : ButtonRenderer
    {
        private GradientDrawable normalDrawable, pressedDrawable;

        public CustomButtonRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);

            if (this.Element is CustomButton customButton)
            {
                var button = this.Control;
                if (button != null)
                {
                    // Create a drawable for the button's normal state
                    this.normalDrawable = new GradientDrawable();
                    this.normalDrawable.SetColor(customButton.BackgroundColor.ToAndroid());
                    this.normalDrawable.SetStroke((int)customButton.BorderWidth, customButton.BorderColor.ToAndroid());
                    this.normalDrawable.SetCornerRadius(customButton.CornerRadius);

                    // Create a drawable for the button's pressed state
                    this.pressedDrawable = new GradientDrawable();
                    this.pressedDrawable.SetColor(customButton.BackgroundColorPressed.ToAndroid());
                    this.pressedDrawable.SetStroke((int)customButton.BorderWidth, customButton.BorderColorPressed.ToAndroid());
                    this.pressedDrawable.SetCornerRadius(customButton.CornerRadius);

                    // Add the drawables to a state list and assign the state list to the button
                    var sld = new StateListDrawable();
                    sld.AddState(new int[] { global::Android.Resource.Attribute.StatePressed }, this.pressedDrawable);
                    sld.AddState(new int[] { }, this.normalDrawable);
                    button.SetBackgroundDrawable(sld);

                    this.UpdateAlignment(customButton);
                }
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (this.Element is CustomButton customButton)
            {
                if (this.normalDrawable != null && this.pressedDrawable != null)
                {
                    if (e.PropertyName == VisualElement.BackgroundColorProperty.PropertyName)
                    {
                        this.normalDrawable.SetColor(customButton.BackgroundColor.ToAndroid());
                    }
                    else if (e.PropertyName == CustomButton.BackgroundColorPressedProperty.PropertyName)
                    {
                        this.pressedDrawable.SetColor(customButton.BackgroundColorPressed.ToAndroid());
                    }
                    else if (e.PropertyName == Button.CornerRadiusProperty.PropertyName)
                    {
                        this.normalDrawable.SetCornerRadius(customButton.CornerRadius);
                        this.pressedDrawable.SetCornerRadius(customButton.CornerRadius);
                    }
                    else if (e.PropertyName == Button.BorderWidthProperty.PropertyName || e.PropertyName == Button.BorderColorProperty.PropertyName
                             || e.PropertyName == CustomButton.BorderColorPressedProperty.PropertyName)
                    {
                        this.normalDrawable.SetStroke((int)customButton.BorderWidth, customButton.BorderColor.ToAndroid());
                        this.pressedDrawable.SetStroke((int)customButton.BorderWidth, customButton.BorderColorPressed.ToAndroid());
                    }
                    else if (e.PropertyName == CustomButton.VerticalContentAlignmentProperty.PropertyName || e.PropertyName == CustomButton.HorizontalContentAlignmentProperty.PropertyName)
                    {
                        this.UpdateAlignment(customButton);
                    }
                }
            }
        }

        private void UpdateAlignment(CustomButton customButton)
        {
            this.Control.Gravity = customButton.VerticalContentAlignment.ToDroidVerticalGravity() |
                                   customButton.HorizontalContentAlignment.ToDroidHorizontalGravity();
        }
    }
}