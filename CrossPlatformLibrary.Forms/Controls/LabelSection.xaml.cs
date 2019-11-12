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

            string newText;
            if(newValue is string newStringValue)
            {
                newText = Device.RuntimePlatform == Device.iOS 
                    ? newStringValue.ToUpperInvariant()
                    : newStringValue;
            }
            else
            {
                newText = null;
            }

            labelSection.Section.Text = newText;
        }

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
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

