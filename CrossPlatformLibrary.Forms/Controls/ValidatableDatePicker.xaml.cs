using System;
using System.Collections.Generic;
using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableDatePicker : GridZero
    {
        public ValidatableDatePicker()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableDatePicker),
                null,
                BindingMode.OneWay,
                null,
                OnPlaceholderPropertyChanged);

        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatableDatePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
        }

        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
        }

        public string AnnotationText
        {
            get
            {
                if (this.Date != null)
                {
                    return this.Placeholder;
                }

                return " ";
            }
        }

        public new static readonly BindableProperty StyleProperty =
            BindableProperty.Create(
                nameof(Style),
                typeof(Style),
                typeof(ValidatableDatePicker),
                default(Style),
                BindingMode.OneWay);

        public new Style Style
        {
            get => (Style)this.GetValue(StyleProperty);
            set => this.SetValue(StyleProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(
                nameof(FontFamily),
                typeof(string),
                typeof(ValidatableDatePicker),
                default(string),
                BindingMode.OneWay);

        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                nameof(FontSize),
                typeof(double),
                typeof(ValidatableDatePicker),
                Font.Default.FontSize,
                BindingMode.OneWay);

        public double FontSize
        {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(
                nameof(FontAttributes),
                typeof(FontAttributes),
                typeof(ValidatableDatePicker),
                FontAttributes.None,
                BindingMode.OneWay);

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)this.GetValue(FontAttributesProperty);
            set => this.SetValue(FontAttributesProperty, (object)value);
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(
                nameof(Date),
                typeof(DateTime?),
                typeof(ValidatableDatePicker),
                null,
                BindingMode.TwoWay,
                null,
                OnDatePropertyChanged);

        private static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            Debug.WriteLine($"OnDatePropertyChanged: oldValue={oldValue}, newValue={newValue}");

            var picker = (ValidatableDatePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
        }

        public DateTime? Date
        {
            get => (DateTime?)this.GetValue(DateProperty);
            set => this.SetValue(DateProperty, value);
        }

        public static readonly BindableProperty ValidityRangeProperty =
            BindableProperty.Create(
                nameof(ValidityRange),
                typeof(DateRange),
                typeof(ValidatableDatePicker),
                default(DateRange),
                BindingMode.OneWay,
                null,
                OnValidityRangePropertyChanged);

        private static void OnValidityRangePropertyChanged(BindableObject bindable, object oldvalue, object newvalue)
        {
            Debug.WriteLine($"OnValidityRangePropertyChanged: oldvalue={oldvalue}, newvalue={newvalue}");
        }

        public DateRange ValidityRange
        {
            get => (DateRange)this.GetValue(ValidityRangeProperty);
            set => this.SetValue(ValidityRangeProperty, value);
        }

        public static readonly BindableProperty IsReadonlyProperty =
            BindableProperty.Create(
                nameof(IsReadonly),
                typeof(bool),
                typeof(ValidatableDatePicker),
                false,
                BindingMode.OneWay);

        public bool IsReadonly
        {
            get => (bool)this.GetValue(IsReadonlyProperty);
            set => this.SetValue(IsReadonlyProperty, value);
        }

        public static readonly BindableProperty ReadonlyTextProperty =
            BindableProperty.Create(
                nameof(ReadonlyText),
                typeof(string),
                typeof(ValidatableDatePicker),
                null,
                BindingMode.OneWay);

        public string ReadonlyText
        {
            get => (string)this.GetValue(ReadonlyTextProperty);
            set => this.SetValue(ReadonlyTextProperty, value);
        }

        public static readonly BindableProperty ValidationErrorsProperty =
            BindableProperty.Create(
                nameof(ValidationErrors),
                typeof(IEnumerable<string>),
                typeof(ValidatableDatePicker),
                default(IEnumerable<string>),
                BindingMode.OneWay);

        public IEnumerable<string> ValidationErrors
        {
            get => (IEnumerable<string>)this.GetValue(ValidationErrorsProperty);
            set => this.SetValue(ValidationErrorsProperty, value);
        }

        public static readonly BindableProperty PickerStyleProperty =
            BindableProperty.Create(
                nameof(PickerStyle),
                typeof(Style),
                typeof(ValidatableDatePicker),
                default(Style),
                BindingMode.OneWay);

        public Style PickerStyle
        {
            get => (Style)this.GetValue(PickerStyleProperty);
            set => this.SetValue(PickerStyleProperty, value);
        }
    }
}