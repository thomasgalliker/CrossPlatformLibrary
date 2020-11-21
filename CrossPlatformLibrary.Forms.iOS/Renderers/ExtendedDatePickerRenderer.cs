using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
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
    ///     Via: https://github.com/tcerdaj/PoolGuy/blob/master/PoolGuy.Mobile.iOS/CustomRenderer/CustomDatePickerRenderer.cs
    /// </summary>
    [Preserve(AllMembers = true)]
    public class ExtendedDatePickerRenderer : DatePickerRenderer
    {
        private static readonly nint CancelButtonTag = 88;
        private static readonly nint ClearButtonTag = 99;

        public new static async void Init()
        {
            var now = DateTime.Now;
        }

        private void AddClearButton()
        {
            var originalToolbar = this.Control.InputAccessoryView as UIToolbar;

            if (originalToolbar != null)
            {
                var newItems = new List<UIBarButtonItem>();

                var view = (ExtendedDatePicker)this.Element;

                UIBarButtonItem cancelButton = null;
                if (!originalToolbar.Items.Any(t => t.Tag == CancelButtonTag))
                {
                    var cancelButtonText = view.CancelButtonText;
                    if (!string.IsNullOrEmpty(cancelButtonText))
                    {
                        cancelButton = new UIBarButtonItem(cancelButtonText, UIBarButtonItemStyle.Plain, (sender, ev) =>
                        {
                            var isNullDate = view.NullableDate == null;
                            view.Unfocus();
                            if (isNullDate)
                            {
                                view.NullableDate = null;
                                this.SetNullableText(view);
                            }
                        })
                        { Tag = CancelButtonTag };
                    }
                }

                UIBarButtonItem clearButton = null;
                if (!originalToolbar.Items.Any(t => t.Tag == ClearButtonTag))
                {
                    var clearButtonText = view.ClearButtonText;
                    if (!string.IsNullOrEmpty(clearButtonText))
                    {
                        clearButton = new UIBarButtonItem(clearButtonText, UIBarButtonItemStyle.Plain, (sender, ev) =>
                        {
                            view.Unfocus();
                            view.Date = DateTime.Now;
                            view.NullableDate = null;
                        })
                        { Tag = ClearButtonTag };
                    }
                }

                if (cancelButton != null || clearButton != null)
                {
                    foreach (var item in originalToolbar.Items)
                    {
                        newItems.Add(item);
                    }

                    if (cancelButton != null)
                    {
                        newItems.Insert(0, cancelButton);
                    }

                    if (clearButton != null)
                    {
                        newItems.Insert(0, clearButton);
                    }

                    originalToolbar.Items = newItems.ToArray();
                    originalToolbar.SetNeedsDisplay();
                }
            }
        }

        protected override void OnElementChanged(ElementChangedEventArgs<DatePicker> e)
        {
            base.OnElementChanged(e);

            if (e.NewElement == null)
            {
                return;
            }

            if (this.Element is ExtendedDatePicker view)
            {
                this.AddClearButton();

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

            if (e.PropertyName == ExtendedDatePicker.HorizontalTextAlignmentProperty.PropertyName)
            {
                this.SetTextAlignment(view);
            }
            else if (e.PropertyName == ExtendedDatePicker.HasBorderProperty.PropertyName)
            {
                this.SetBorder(view);
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
                this.SetPlaceholder(view);
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
                var localDateTime = view.NullableDate.Value;
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