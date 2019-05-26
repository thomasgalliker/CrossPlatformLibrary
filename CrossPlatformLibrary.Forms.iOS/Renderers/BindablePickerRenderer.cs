using System.ComponentModel;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(BindablePicker), typeof(BindablePickerRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    public class BindablePickerRenderer : PickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            if (this.Element is BindablePicker bindablePicker)
            {
                this.SetFont(bindablePicker);
                this.SetPlaceholder(bindablePicker);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (BindablePicker)this.Element;

            if (e.PropertyName == BindablePicker.FontFamilyProperty.PropertyName || e.PropertyName == BindablePicker.FontSizeProperty.PropertyName)
            {
                this.SetFont(view);
            }
            else if (e.PropertyName == BindablePicker.TitleProperty.PropertyName || e.PropertyName == BindablePicker.PlaceholderTextColorProperty.PropertyName)
            {
                this.SetPlaceholder(view);
            }
        }

        private void SetFont(BindablePicker bindablePicker)
        {
            var font = Font.OfSize(bindablePicker.FontFamily, bindablePicker.FontSize);

            UIFont uiFont;
            if (font != Font.Default && (uiFont = font.ToUIFont()) != null)
            {
                this.Control.Font = uiFont;
            }
            else if (font == Font.Default)
            {
                this.Control.Font = UIFont.SystemFontOfSize(17f);
            }
        }

        /// <summary>
        ///     Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholder(BindablePicker view)
        {
            if (!string.IsNullOrEmpty(view.Title))
            {
                var foregroundUiColor = view.PlaceholderTextColor.ToUIColor();
                var backgroundUiColor = view.BackgroundColor.ToUIColor();
                var targetFont = this.Control.Font;
                this.Control.AttributedPlaceholder = new NSAttributedString(view.Title, targetFont, foregroundUiColor, backgroundUiColor);
            }
        }
    }
}