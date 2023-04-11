using System;
using System.ComponentModel;
using Android.App;
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
    ///     Via: https://github.com/tcerdaj/PoolGuy/blob/master/PoolGuy.Mobile.Android/CustomRenderer/CustomDatePickerRenderer.cs
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        private static readonly int negativeButtonId = (int)DialogButtonType.Negative;

        public static new async void Init()
        {
            var now = DateTime.Now;
        }

        public ExtendedDatePickerRenderer(Context context) : base(context)
        {
        }

        protected override DatePickerDialog CreateDatePickerDialog(int year, int month, int day)
        {
            var dialog = base.CreateDatePickerDialog(year, month, day);
            var view = (ExtendedDatePicker)this.Element;

            // Override cancel button functionality.
            // We don't want to return a value if cancel is pressed.
            var cancelButtonText = !string.IsNullOrEmpty(view.CancelButtonText)
                ? view.CancelButtonText 
                : this.Resources.GetString(global::Android.Resource.String.Cancel);

            dialog.SetButton(negativeButtonId, cancelButtonText, (x, y) =>
            {
                if (y.Which == negativeButtonId)
                {
                    var isNullDate = view.NullableDate == null;
                    view.Unfocus();
                    if (isNullDate)
                    {
                        view.NullableDate = null;
                        this.SetNullableText(view);
                    }
                }
            });

            // Add clear button which resets the currently set value.
            // If ClearButtonText is null/empty, don't add the clear button.
            var clearButtonText = view.ClearButtonText;
            if (!string.IsNullOrEmpty(clearButtonText))
            {
                var neutralButtonId = (int)DialogButtonType.Neutral;
                dialog.SetButton(neutralButtonId, clearButtonText, (x, y) =>
                {
                    if (y.Which == neutralButtonId)
                    {
                        view.Unfocus();

                        view.NullableDate = null;
                        this.SetNullableText(view);
                    }
                });
            }

            return dialog;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.DatePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Element is ExtendedDatePicker view)
            {
                this.SetTextAlignment(view);
                this.SetNullableText(view);
                this.SetPlaceholder(view);
                this.SetPlaceholderTextColor(view);
            }
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            var view = (ExtendedDatePicker)this.Element;

            if (e.PropertyName == ExtendedDatePicker.HorizontalTextAlignmentProperty.PropertyName)
            {
                this.SetTextAlignment(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.NullableDateProperty.PropertyName ||
               e.PropertyName == DatePicker.DateProperty.PropertyName ||
               e.PropertyName == DatePicker.FormatProperty.PropertyName)
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
            switch (view.HorizontalTextAlignment)
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