using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableDateTimePicker : Grid
    {
        public ValidatableDateTimePicker()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableDateTimePicker),
                string.Empty,
                BindingMode.OneWay,
                null,
                OnPlaceholderPropertyChanged);

        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatableDateTimePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
        }

        public string Placeholder
        {
            get { return (string)this.GetValue(PlaceholderProperty); }
            set { this.SetValue(PlaceholderProperty, value); }
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
                typeof(ValidatableDateTimePicker),
                default(Style),
                BindingMode.OneWay);

        public new Style Style
        {
            get { return (Style)this.GetValue(StyleProperty); }
            set { this.SetValue(StyleProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(
                nameof(FontFamily),
                typeof(string),
                typeof(ValidatableDateTimePicker),
                default(string),
                BindingMode.OneWay);

        public string FontFamily
        {
            get { return (string)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                nameof(FontSize),
                typeof(double),
                typeof(ValidatableDateTimePicker),
                Font.Default.FontSize,
                BindingMode.OneWay);

        public double FontSize
        {
            get { return (double)this.GetValue(FontSizeProperty); }
            set { this.SetValue(FontSizeProperty, value); }
        }

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(
                nameof(FontAttributes),
                typeof(FontAttributes),
                typeof(ValidatableDateTimePicker),
                FontAttributes.None,
                BindingMode.OneWay);

        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)this.GetValue(FontAttributesProperty); }
            set { this.SetValue(FontAttributesProperty, (object)value); }
        }

        public static readonly BindableProperty DateProperty =
            BindableProperty.Create(
                nameof(Date),
                typeof(DateTime?),
                typeof(ValidatableDateTimePicker),
                null,
                BindingMode.TwoWay,
                null,
                OnDatePropertyChanged);

        private static void OnDatePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatableDateTimePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
        }

        public DateTime? Date
        {
            get { return (DateTime?)this.GetValue(DateProperty); }
            set { this.SetValue(DateProperty, value); }
        }

        public static readonly BindableProperty TimeProperty =
            BindableProperty.Create(
                nameof(Time),
                typeof(TimeSpan?),
                typeof(ValidatableDateTimePicker),
                null,
                BindingMode.TwoWay,
                null,
                OnTimePropertyChanged);

        private static void OnTimePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatableDateTimePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
        }

        public TimeSpan? Time
        {
            get { return (TimeSpan?)this.GetValue(TimeProperty); }
            set { this.SetValue(TimeProperty, value); }
        }

        public static readonly BindableProperty IsReadonlyProperty =
            BindableProperty.Create(
                nameof(IsReadonly),
                typeof(bool),
                typeof(ValidatableDateTimePicker),
                false,
                BindingMode.OneWay);

        public bool IsReadonly
        {
            get { return (bool)this.GetValue(IsReadonlyProperty); }
            set { this.SetValue(IsReadonlyProperty, value); }
        }

        public static readonly BindableProperty ReadonlyTextProperty =
            BindableProperty.Create(
                nameof(ReadonlyText),
                typeof(string),
                typeof(ValidatableDateTimePicker),
                null,
                BindingMode.OneWay);

        public string ReadonlyText
        {
            get { return (string)this.GetValue(ReadonlyTextProperty); }
            set { this.SetValue(ReadonlyTextProperty, value); }
        }

        public static readonly BindableProperty ValidationErrorsProperty =
            BindableProperty.Create(
                nameof(ValidationErrors),
                typeof(IEnumerable<string>),
                typeof(ValidatableDateTimePicker),
                default(IEnumerable<string>),
                BindingMode.OneWay);

        public IEnumerable<string> ValidationErrors
        {
            get { return (IEnumerable<string>)this.GetValue(ValidationErrorsProperty); }
            set { this.SetValue(ValidationErrorsProperty, value); }
        }
    }
}