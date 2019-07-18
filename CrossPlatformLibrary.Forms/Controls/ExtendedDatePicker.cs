using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    ///     Extended DatePicker for Nullable Values
    ///     Via: https://forums.xamarin.com/discussion/20028/datepicker-possible-to-bind-to-nullable-date-value
    ///     Via: https://github.com/XLabs/Xamarin-Forms-Labs/wiki/ExtendedEntry
    /// </summary>
    public class ExtendedDatePicker : DatePicker
    {
        public static readonly BindableProperty FontProperty =
            BindableProperty.Create(
                nameof(Font),
                typeof(Font),
                typeof(ExtendedDatePicker),
                new Font());

        public static readonly BindableProperty NullableDateProperty =
            BindableProperty.Create(
                nameof(NullableDate),
                typeof(DateTime?),
                typeof(ExtendedDatePicker),
                null,
                BindingMode.TwoWay);

        public static readonly BindableProperty ValidityRangeProperty =
            BindableProperty.Create(
                nameof(ValidityRange),
                typeof(DateRange),
                typeof(ExtendedDatePicker),
                null,
                BindingMode.OneWay,
                null,
                OnValidityRangePropertyChanged);

        private static void OnValidityRangePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            Debug.WriteLine($"OnValidityRangePropertyChanged: oldvalue={oldvalue}, newvalue={newvalue}");

            if (newvalue is DateRange dateRange)
            {
                var picker = (ExtendedDatePicker)bindable;
                picker.MaximumDate = DateTime.MaxValue;
                picker.MinimumDate = DateTime.MinValue;

                picker.MaximumDate = dateRange.End;
                picker.MinimumDate = dateRange.Start;
            }
        }

        public DateRange ValidityRange
        {
            get { return (DateRange)this.GetValue(ValidityRangeProperty); }
            set { this.SetValue(ValidityRangeProperty, value); }
        }

        public static readonly BindableProperty XAlignProperty =
            BindableProperty.Create(
                nameof(XAlign),
                typeof(TextAlignment),
                typeof(ExtendedDatePicker),
                TextAlignment.Start);

        public static readonly BindableProperty HasBorderProperty =
            BindableProperty.Create(
                nameof(HasBorder),
                typeof(bool),
                typeof(ExtendedDatePicker),
                true);

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ExtendedDatePicker),
                string.Empty,
                BindingMode.OneWay);

        public static readonly BindableProperty PlaceholderTextColorProperty =
            BindableProperty.Create(
                nameof(PlaceholderTextColor),
                typeof(Color),
                typeof(ExtendedDatePicker),
                Color.Default);

        public DateTime? NullableDate
        {
            get { return (DateTime?)this.GetValue(NullableDateProperty); }
            set
            {
                if (value != this.NullableDate)
                {
                    this.SetValue(NullableDateProperty, value);
                    this.UpdateDate();
                }
            }
        }

        public Font Font
        {
            get { return (Font)this.GetValue(FontProperty); }
            set { this.SetValue(FontProperty, value); }
        }

        public TextAlignment XAlign
        {
            get { return (TextAlignment)this.GetValue(XAlignProperty); }
            set { this.SetValue(XAlignProperty, value); }
        }

        public bool HasBorder
        {
            get { return (bool)this.GetValue(HasBorderProperty); }
            set { this.SetValue(HasBorderProperty, value); }
        }

        public string Placeholder
        {
            get { return (string)this.GetValue(PlaceholderProperty); }
            set { this.SetValue(PlaceholderProperty, value); }
        }

        public Color PlaceholderTextColor
        {
            get { return (Color)this.GetValue(PlaceholderTextColorProperty); }
            set { this.SetValue(PlaceholderTextColorProperty, value); }
        }

        protected override void OnBindingContextChanged()
        {
            base.OnBindingContextChanged();
            this.UpdateDate();
        }

        private bool isPickerDialogOpen = false;

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName == IsFocusedProperty.PropertyName)
            {
                if (this.IsFocused)
                {
                    if (this.NullableDate == null || this.NullableDate == DateTime.MinValue)
                    {
                        this.Date = DateTime.Today;
                    }

                    this.isPickerDialogOpen = true;
                }
                else
                {
                    this.OnPropertyChanged(DateProperty.PropertyName);
                    this.isPickerDialogOpen = false;
                }
            }

            if (propertyName == DateProperty.PropertyName)
            {
                if (this.isPickerDialogOpen)
                {
                    this.NullableDate = this.Date;
                }
            }

            if (propertyName == NullableDateProperty.PropertyName)
            {
                this.UpdateDate();
            }
        }

        private void UpdateDate()
        {
            if (this.NullableDate.HasValue)
            {
                this.Date = this.NullableDate.Value;
            }
        }
    }
}