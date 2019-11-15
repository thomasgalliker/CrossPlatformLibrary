using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LabelSection : ContentView
    {
        public LabelSection()
        {
            this.InitializeComponent();
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
            nameof(Text),
            typeof(string),
            typeof(LabelSection),
            null,
            BindingMode.OneWay,
            null,
            OnTextPropertyChanged);

        private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var labelSection = (LabelSection)bindable;

            UpdateSectionText(labelSection, newValue as string, labelSection.IsTextUpperCase);
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static readonly BindableProperty IsTextUpperCaseProperty =
            BindableProperty.Create(
                nameof(IsTextUpperCase),
                typeof(bool),
                typeof(LabelSection),
                GetPlatformDefaultIsTextUpperCase(),
                BindingMode.OneWay,
                null,
                OnIsTextUpperCasePropertyChanged);

        private static void OnIsTextUpperCasePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(newValue is bool upperCase))
            {
                return;
            }

            var labelSection = (LabelSection)bindable;
            UpdateSectionText(labelSection, labelSection.Text, upperCase);
        }

        private static bool GetPlatformDefaultIsTextUpperCase()
        {
            return Device.RuntimePlatform == Device.iOS;
        }

        public bool IsTextUpperCase
        {
            get => (bool)this.GetValue(IsTextUpperCaseProperty);
            set => this.SetValue(IsTextUpperCaseProperty, value);
        }

        private static void UpdateSectionText(LabelSection labelSection, string text, bool isUpperCase)
        {
            var newText = isUpperCase
                ? text?.ToUpperInvariant()
                : text;

            if (labelSection.Section is Label label)
            {
                label.Text = newText;
            }
        }

        public static readonly BindableProperty LabelStyleProperty =
            BindableProperty.Create(
                nameof(LabelStyle),
                typeof(Style),
                typeof(LabelSection),
                default(Style));

        public Style LabelStyle
        {
            get => (Style)this.GetValue(LabelStyleProperty);
            set => this.SetValue(LabelStyleProperty, value);
        }
    }
}

