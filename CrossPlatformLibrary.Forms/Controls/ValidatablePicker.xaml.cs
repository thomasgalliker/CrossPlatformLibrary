using System.Collections;
using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    /// <summary>
    ///     Similar solution can be found here:
    ///     https://github.com/XamFormsExtended/Xfx.Controls
    /// </summary>
    public partial class ValidatablePicker : Grid
    {
        public ValidatablePicker()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatablePicker),
                string.Empty,
                BindingMode.OneWay,
                null,
                OnPlaceholderPropertyChanged);

        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatablePicker)bindable;
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
                if (this.SelectedItem != null)
                {
                    return this.Placeholder;
                }

                return " ";
            }
        }

        public static readonly BindableProperty PickerStyleProperty =
            BindableProperty.Create(
                nameof(PickerStyle),
                typeof(Style),
                typeof(ValidatablePicker),
                default(Style),
                BindingMode.OneWay);

        public Style PickerStyle
        {
            get => (Style)this.GetValue(PickerStyleProperty);
            set => this.SetValue(PickerStyleProperty, value);
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(
                nameof(FontFamily),
                typeof(string),
                typeof(ValidatablePicker),
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
                typeof(ValidatablePicker),
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
                typeof(ValidatablePicker),
                FontAttributes.None,
                BindingMode.OneWay);

        public FontAttributes FontAttributes
        {
            get { return (FontAttributes)this.GetValue(FontAttributesProperty); }
            set { this.SetValue(FontAttributesProperty, (object)value); }
        }

        public static readonly BindableProperty ItemsSourceProperty =
            BindableProperty.Create(
                nameof(ItemsSource),
                typeof(IEnumerable),
                typeof(ValidatablePicker),
                null,
                BindingMode.OneWay,
                null,
                OnItemsSourcePropertyChanged);

        private static void OnItemsSourcePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatablePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.Placeholder));
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
        }

        public IEnumerable ItemsSource
        {
            get { return (IEnumerable)this.GetValue(ItemsSourceProperty); }
            set { this.SetValue(ItemsSourceProperty, value); }
        }

        public static readonly BindableProperty SelectedItemProperty =
            BindableProperty.Create(
                nameof(SelectedItem),
                typeof(object),
                typeof(ValidatablePicker),
                null,
                BindingMode.TwoWay,
                null,
                OnSelectedItemPropertyChanged);

        private static void OnSelectedItemPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var picker = (ValidatablePicker)bindable;
            picker.OnPropertyChanged(nameof(picker.Placeholder));
            picker.OnPropertyChanged(nameof(picker.AnnotationText));
            picker.OnPropertyChanged(nameof(picker.ReadonlyText));
        }

        public object SelectedItem
        {
            get { return this.GetValue(SelectedItemProperty); }
            set
            {
                this.SetValue(SelectedItemProperty, value);
                this.OnPropertyChanged(nameof(this.AnnotationText));
            }
        }

        public static readonly BindableProperty SelectedValueProperty =
            BindableProperty.Create(
                nameof(SelectedValue),
                typeof(object),
                typeof(ValidatablePicker),
                null,
                BindingMode.TwoWay);

        public object SelectedValue
        {
            get { return this.GetValue(SelectedValueProperty); }
            set { this.SetValue(SelectedValueProperty, value); }
        }

        public static readonly BindableProperty SelectedValuePathProperty =
            BindableProperty.Create(
                nameof(SelectedValuePath),
                typeof(string),
                typeof(ValidatablePicker),
                null,
                BindingMode.OneWay);

        public string SelectedValuePath
        {
            get { return (string)this.GetValue(SelectedValuePathProperty); }
            set { this.SetValue(SelectedValuePathProperty, value); }
        }

        public static readonly BindableProperty DisplayMemberPathProperty =
            BindableProperty.Create(
                nameof(DisplayMemberPath),
                typeof(string),
                typeof(ValidatablePicker),
                null,
                BindingMode.OneWay);

        public string DisplayMemberPath
        {
            get { return (string)this.GetValue(DisplayMemberPathProperty); }
            set { this.SetValue(DisplayMemberPathProperty, value); }
        }

        public static readonly BindableProperty IsReadonlyProperty =
            BindableProperty.Create(
                nameof(IsReadonly),
                typeof(bool),
                typeof(ValidatablePicker),
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
                typeof(ValidatablePicker),
                null,
                BindingMode.OneWay);

        public string ReadonlyText
        {
            get
            {
                var readonlyText = (string)this.GetValue(ReadonlyTextProperty);
                if (readonlyText == null)
                {
                    // In case readonly text is null, we try to take SelectedItem as ReadonlyText
                    readonlyText = this.SelectedItem?.ToString();
                }
                return readonlyText;
            }
            set { this.SetValue(ReadonlyTextProperty, value); }
        }

        public static readonly BindableProperty ValidationErrorsProperty =
            BindableProperty.Create(
                nameof(ValidationErrors),
                typeof(IEnumerable<string>),
                typeof(ValidatablePicker),
                default(IEnumerable<string>),
                BindingMode.OneWay);

        public IEnumerable<string> ValidationErrors
        {
            get { return (IEnumerable<string>)this.GetValue(ValidationErrorsProperty); }
            set { this.SetValue(ValidationErrorsProperty, value); }
        }
    }
}