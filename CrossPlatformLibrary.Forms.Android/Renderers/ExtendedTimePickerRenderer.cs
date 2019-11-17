using System;
using System.ComponentModel;
using Android.Content;
using Android.Views;
using CrossPlatformLibrary.Forms.Android.Renderers;
using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(ExtendedTimePicker), typeof(ExtendedTimePickerRenderer))]

namespace CrossPlatformLibrary.Forms.Android.Renderers
{
    public class ExtendedTimePickerRenderer : TimePickerRenderer
    {
        public ExtendedTimePickerRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<TimePicker> e)
        {
            base.OnElementChanged(e);

            if (this.Element is ExtendedTimePicker view)
            {
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

            var view = (ExtendedTimePicker)this.Element;

            if (e.PropertyName == ExtendedTimePicker.XAlignProperty.PropertyName)
            {
                this.SetTextAlignment(view);
            }
            // else if (e.PropertyName == ExtendedTimePicker.HasBorderProperty.PropertyName)
            //  SetBorder(view);
            else if (e.PropertyName == ExtendedTimePicker.NullableTimeProperty.PropertyName || e.PropertyName == TimePicker.FormatProperty.PropertyName)
            {
                this.SetNullableText(view);
            }
            else if (e.PropertyName == ExtendedTimePicker.PlaceholderProperty.PropertyName)
            {
                this.SetPlaceholder(view);
            }
            else if (e.PropertyName == ExtendedTimePicker.PlaceholderTextColorProperty.PropertyName)
            {
                this.SetPlaceholderTextColor(view);
            }
        }

        /// <summary>
        ///     Sets the text alignment.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetTextAlignment(ExtendedTimePicker view)
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
        ///     Set text based on nullable value
        /// </summary>
        /// <param name="view"></param>
        private void SetNullableText(ExtendedTimePicker view)
        {
            var format = this.Element.Format;
            if (view.NullableTime == null || format == string.Empty)
            {
                this.Control.Text = string.Empty;
            }
            else
            {
                var today = DateTime.Today;
                today = today.Add(view.NullableTime.Value);

                var localDateTime = today.ToLocalTime();
                this.Control.Text = localDateTime.ToString(format);
            }
        }

        /// <summary>
        ///     Set the placeholder
        /// </summary>
        /// <param name="view"></param>
        private void SetPlaceholder(ExtendedTimePicker view)
        {
            this.Control.Hint = view.Placeholder;
        }

        /// <summary>
        ///     Sets the color of the placeholder text.
        /// </summary>
        /// <param name="view">The view.</param>
        private void SetPlaceholderTextColor(ExtendedTimePicker view)
        {
            if (view.PlaceholderTextColor != Color.Default)
            {
                this.Control.SetHintTextColor(view.PlaceholderTextColor.ToAndroid());
            }
        }
    }
}