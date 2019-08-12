using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedDatePicker), typeof(ExtendedDatePickerRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
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

        public ExtendedDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Element is ExtendedDatePicker view)
            {
                this.SetFont(view);
                this.SetTextAlignment(view);
                // SetBorder(view);
                this.SetNullableText(view);
                this.SetPlaceholder(view);
                this.SetPlaceholderTextColor(view);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (ExtendedDatePicker)this.Element;

            if (e.PropertyName == ExtendedDatePicker.FontProperty.PropertyName)
            {
                this.SetFont(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.XAlignProperty.PropertyName)
            {
                this.SetTextAlignment(view);
            }
            // else if (e.PropertyName == ExtendedDatePicker.HasBorderProperty.PropertyName)
            //  SetBorder(view);
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
                this.SetPlaceholderTextColor(view);
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
                case Xamarin.Forms.TextAlignment.Center:
                    this.Control.Gravity = GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    this.Control.Gravity = GravityFlags.End;
                    break;
                case Xamarin.Forms.TextAlignment.Start:
                    this.Control.Gravity = GravityFlags.Start;
                    break;
            }
        }

        /// <summary>
        ///     Sets the font.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetFont(ExtendedDatePicker view)
        {
            if (view.Font != Font.Default)
            {
                this.Control.TextSize = view.Font.ToScaledPixel();
                this.Control.Typeface = view.Font.ToTypeface();
            }
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

        private void SetPlaceholder(ExtendedDatePicker view)
        {
            this.Control.Hint = view.Placeholder;
        }

        private void SetPlaceholderTextColor(ExtendedDatePicker view)
        {
            if (view.PlaceholderTextColor != Color.Default)
            {
                this.Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}