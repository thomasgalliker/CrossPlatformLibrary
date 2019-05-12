using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableLabel : Grid
    {
        public ValidatableLabel()
        {
            this.InitializeComponent();
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
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
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

        public new static readonly BindableProperty StyleProperty =
            BindableProperty.Create(
                nameof(Style),
                typeof(Style),
                typeof(ValidatableLabel),
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
                typeof(ValidatableLabel),
                default(string),
                BindingMode.OneWay);

        public string FontFamily
        {
            get { return (string)this.GetValue(FontFamilyProperty); }
            set { this.SetValue(FontFamilyProperty, value); }
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
            get { return (LineBreakMode)this.GetValue(LineBreakModeProperty); }
            set { this.SetValue(LineBreakModeProperty, value); }
        }
    }
}