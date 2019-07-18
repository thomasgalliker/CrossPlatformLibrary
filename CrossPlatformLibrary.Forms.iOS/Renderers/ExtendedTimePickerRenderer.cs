using System;
using System.ComponentModel;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedTimePicker), typeof(ExtendedTimePickerRenderer))]
namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    /// <summary>
    ///  Extended TimePicker Renderer for Nullable Values
    ///  Via: https://forums.xamarin.com/discussion/20028/datepicker-possible-to-bind-to-nullable-date-value
    ///  Via: https://github.com/XLabs/Xamarin-Forms-Labs/wiki/ExtendedEntry
    /// </summary>
    public class ExtendedTimePickerRenderer : TimePickerRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            var view = this.Element as ExtendedTimePicker;

            if (view != null)
            {
                this.SetFont(view);
                this.SetTextAlignment(view);
                this.SetBorder(view);
                this.SetNullableText(view);
                this.SetPlaceholderTextColor(view);

                this.ResizeHeight();
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (ExtendedTimePicker)this.Element;

            if (e.PropertyName == ExtendedTimePicker.FontProperty.PropertyName)
                this.SetFont(view);
            else if (e.PropertyName == ExtendedTimePicker.XAlignProperty.PropertyName)
                this.SetTextAlignment(view);
            else if (e.PropertyName == ExtendedTimePicker.HasBorderProperty.PropertyName)
                this.SetBorder(view);
            else if (e.PropertyName == ExtendedTimePicker.NullableTimeProperty.PropertyName || e.PropertyName == TimePicker.FormatProperty.PropertyName)
            {
                this.SetNullableText(view);
            }
            else if (e.PropertyName == ExtendedTimePicker.PlaceholderTextColorProperty.PropertyName)
                this.SetPlaceholderTextColor(view);

            this.ResizeHeight();
        }

        /// <summary>
        /// Sets the text alignment.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetTextAlignment(ExtendedTimePicker view)
        {
            switch (view.XAlign)
            {
                case TextAlignment.Center:
                    this.Control.TextAlignment = UITextAlignment.Center;
                    break;
                case TextAlignment.End:
                    this.Control.TextAlignment = UITextAlignment.Right;
                    break;
                case TextAlignment.Start:
                    this.Control.TextAlignment = UITextAlignment.Left;
                    break;
            }
        }

        /// <summary>
        /// Sets the font.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetFont(ExtendedTimePicker view)
        {
            UIFont uiFont;
            if (view.Font != Font.Default && (uiFont = view.Font.ToUIFont()) != null)
            {
                this.Control.Font = uiFont;
            }
            else if (view.Font == Font.Default)
            {
                this.Control.Font = UIFont.SystemFontOfSize(17f);
            }
        }

        /// <summary>
        /// Sets the border.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetBorder(ExtendedTimePicker view)
        {
            this.Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        /// <summary>
        /// Set text based on nullable value
        /// </summary>
        /// <param name="view"></param>
        private void SetNullableText(ExtendedTimePicker view)
        {
            var today = DateTime.Today;
            if (view.NullableTime != null)
            {
                today = today.Add(view.NullableTime.Value);
            }

            if (this.Element.Format == "")
            {
                this.Control.Text = "";
            }
            else
            {
                this.Control.Text = today.ToString(this.Element.Format);
            }
        }

        /// <summary>
        /// Resizes the height.
        /// </summary>
        private void ResizeHeight()
        {
            if (this.Element.HeightRequest >= 0) return;

            var height = Math.Max(this.Bounds.Height,
                new UITextField { Font = this.Control.Font }.IntrinsicContentSize.Height) * 2;

            this.Control.Frame = new CGRect(0.0f, 0.0f, (nfloat)this.Element.Width, (nfloat)height);

            this.Element.HeightRequest = height;
        }

        /// <summary>
        /// Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(ExtendedTimePicker view)
        {
            if (!string.IsNullOrEmpty(view.Placeholder))
            {
                var foregroundUIColor = view.PlaceholderTextColor.ToUIColor();
                var backgroundUIColor = view.BackgroundColor.ToUIColor();
                var targetFont = this.Control.Font;
                this.Control.AttributedPlaceholder = new NSAttributedString(view.Placeholder, targetFont, foregroundUIColor, backgroundUIColor);
            }
        }
    }
}
