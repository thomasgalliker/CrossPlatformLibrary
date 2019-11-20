using System;
using System.ComponentModel;
using CoreGraphics;
using CrossPlatformLibrary.Forms.Controls;
using CrossPlatformLibrary.Forms.iOS.Renderers;
using Foundation;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]

namespace CrossPlatformLibrary.Forms.iOS.Renderers
{
    /// <summary>
    ///     Extended DatePicker Renderer for Nullable Values
    ///     Via: https://forums.xamarin.com/discussion/20028/datepicker-possible-to-bind-to-nullable-date-value
    ///     Via: https://github.com/XLabs/Xamarin-Forms-Labs/wiki/ExtendedEntry
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        public new static async void Init()
        {
            var now = DateTime.Now;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Element is ExtendedDatePicker view)
            {
                this.SetTextAlignment(view);
                this.SetBorder(view);
                this.SetNullableText(view);
                this.SetPlaceholder(view);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (ExtendedDatePicker)this.Element;

            if (e.PropertyName == ExtendedDatePicker.XAlignProperty.PropertyName)
            {
                this.SetTextAlignment(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.HasBorderProperty.PropertyName)
            {
                this.SetBorder(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.NullableDateProperty.PropertyName || e.PropertyName == DatePicker.FormatProperty.PropertyName)
            {
                this.SetNullableText(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.PlaceholderProperty.PropertyName)
            {
                this.SetPlaceholder(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.PlaceholderTextColorProperty.PropertyName)
            {
                this.SetPlaceholder(view);
            }
        }

        /// <summary>
        ///     Sets the text alignment.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetTextAlignment(ExtendedDatePicker view)
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
        ///     Sets the border.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetBorder(ExtendedDatePicker view)
        {
            this.Control.BorderStyle = view.HasBorder ? UITextBorderStyle.RoundedRect : UITextBorderStyle.None;
        }

        /// <summary>
        ///     Set text based on nullable value
        /// </summary>
        private void SetNullableText(ExtendedDatePicker view)
        {
            var format = this.Element.Format;
            if (view.NullableDate != null && view.NullableDate.Value != DateTime.MinValue && !string.IsNullOrEmpty(format))
            {
                var localDateTime = view.NullableDate.Value.ToLocalTime();
                this.Control.Text = localDateTime.ToString(format);
            }
            else
            {
                this.Control.Text = string.Empty;
            }
        }

        /// <summary>
        ///     Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholder(ExtendedDatePicker view)
        {
            if (!string.IsNullOrEmpty(view.Placeholder))
            {
                var foregroundUiColor = view.PlaceholderTextColor.ToUIColor();
                var backgroundUiColor = view.BackgroundColor.ToUIColor();
                var targetFont = this.Control.Font;
                this.Control.AttributedPlaceholder = new NSAttributedString(view.Placeholder, targetFont, foregroundUiColor, backgroundUiColor);
            }
        }
    }
}