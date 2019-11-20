using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class ValidatableLabel : GridZero
    {
        public ValidatableLabel()
        {
            this.InitializeComponent();
            this.DebugLayoutBounds();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(ValidatableLabel),
                propertyChanged: OnTextPropertyChanged);
        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var validatableLabel = (ValidatableLabel)bindable;
            validatableLabel.OnPropertyChanged(nameof(validatableLabel.AnnotationText));
        }

        public static readonly BindableProperty PlaceholderProperty =
            BindableProperty.Create(
                nameof(Placeholder),
                typeof(string),
                typeof(ValidatableLabel),
                propertyChanged: OnPlaceholderPropertyChanged);

        public string Placeholder
        {
            get => (string)this.GetValue(PlaceholderProperty);
            set => this.SetValue(PlaceholderProperty, value);
        }

        private static void OnPlaceholderPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var validatableLabel = (ValidatableLabel)bindable;
            validatableLabel.OnPropertyChanged(nameof(validatableLabel.AnnotationText));
        }

        public string AnnotationText
        {
            get
            {
                if (!string.IsNullOrEmpty(this.Text))
                {
                    return this.Placeholder;
                }

                return " ";
            }
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
            get => (Style)this.GetValue(LabelStyleProperty);
            set => this.SetValue(LabelStyleProperty, value);
        }
    }
}