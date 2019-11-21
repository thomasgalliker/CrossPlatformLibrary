using System;
using System.Collections.Generic;
using CrossPlatformLibrary.Forms.Tools;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableDateTimePicker : GridZero
    {
        public ValidatableDateTimePicker()
        {
            this.InitializeComponent();
            this.DebugLayoutBounds();

            PlatformHelper.RunOnPlatform((Device.Android, () => { this.DatePicker.HeightRequest = -1; }));
            PlatformHelper.RunOnPlatform((Device.Android, () => { this.TimePicker.HeightRequest = -1; }));
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

        public static readonly BindableProperty DatePickerStyleProperty =
            BindableProperty.Create(
                nameof(DatePickerStyle),
                typeof(Style),
                typeof(ValidatableDateTimePicker),
                default(Style));

        public Style DatePickerStyle
        {
            get => (Style)this.GetValue(DatePickerStyleProperty);
            set => this.SetValue(DatePickerStyleProperty, value);
        }

        public static readonly BindableProperty TimePickerStyleProperty =
            BindableProperty.Create(
                nameof(TimePickerStyle),
                typeof(Style),
                typeof(ValidatableDateTimePicker),
                default(Style));

        public Style TimePickerStyle
        {
            get => (Style)this.GetValue(TimePickerStyleProperty);
            set => this.SetValue(TimePickerStyleProperty, value);
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
            get => (DateTime?)this.GetValue(DateProperty);
            set => this.SetValue(DateProperty, value);
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
            get => (TimeSpan?)this.GetValue(TimeProperty);
            set => this.SetValue(TimeProperty, value);
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
            get => (bool)this.GetValue(IsReadonlyProperty);
            set => this.SetValue(IsReadonlyProperty, value);
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
            get => (string)this.GetValue(ReadonlyTextProperty);
            set => this.SetValue(ReadonlyTextProperty, value);
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
            get => (IEnumerable<string>)this.GetValue(ValidationErrorsProperty);
            set => this.SetValue(ValidationErrorsProperty, value);
        }
    }
}