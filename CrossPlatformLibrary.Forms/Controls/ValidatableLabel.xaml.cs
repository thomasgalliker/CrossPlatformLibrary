using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableLabel : GridZero
    {
        public ValidatableLabel()
        {
            this.InitializeComponent();
            //this.SetDynamicResource(LabelStyleProperty, "EntryLabelStyle");
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ValidatableLabel),
                string.Empty,
                BindingMode.OneWay);

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableLabel),
                string.Empty,
                BindingMode.OneWay);

        public string Placeholder
        {
            get { return (string)this.GetValue(PlaceholderProperty); }
            set { this.SetValue(PlaceholderProperty, value); }
        }

        public static readonly BindableProperty LabelStyleProperty =
            BindableProperty.Create(
                nameof(LabelStyle),
                typeof(Style),
                typeof(ValidatableLabel),
                default(Style),
                BindingMode.OneWay);

        public Style LabelStyle
        {
            get { return (Style)this.GetValue(LabelStyleProperty); }
            set { this.SetValue(LabelStyleProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(
                nameof(FontFamily),
                typeof(string),
                typeof(ValidatableLabel),
                default(string),
                BindingMode.OneWay);

        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(
                nameof(FontAttributes),
                typeof(FontAttributes),
                typeof(ValidatableLabel),
                FontAttributes.None,
                BindingMode.OneWay);

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)this.GetValue(FontAttributesProperty);
            set => this.SetValue(FontAttributesProperty, value);
        }

        public static readonly BindableProperty LineBreakModeProperty =
            BindableProperty.Create(
                nameof(LineBreakMode),
                typeof(LineBreakMode),
                typeof(ValidatableLabel),
                LineBreakMode.TailTruncation,
                BindingMode.OneWay);

        public LineBreakMode LineBreakMode
        {
            get => (LineBreakMode)this.GetValue(LineBreakModeProperty);
            set => this.SetValue(LineBreakModeProperty, value);
        }
    }
}