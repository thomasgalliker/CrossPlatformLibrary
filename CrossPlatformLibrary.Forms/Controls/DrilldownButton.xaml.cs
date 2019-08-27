using System.Windows.Input;
using CrossPlatformLibrary.Forms.Resources;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class DrilldownButton : CardView
    {
        public DrilldownButton()
        {
            this.InitializeComponent();

            // Hack: OnPlatform lacks of support for DynamicResource bindings!
            if (Device.RuntimePlatform == Device.Android)
            {
                this.ActivityIndicator.SetDynamicResource(ActivityIndicator.ColorProperty, ThemeConstants.Color.SECONDARY);
            }
        }

        public static readonly BindableProperty TextProperty =
         BindableProperty.Create(
             nameof(Text),
             typeof(string),
             typeof(DrilldownButton),
             string.Empty,
             BindingMode.OneWay);

        public string Text
        {
            get => (string)this.GetValue(TextProperty);
            set => this.SetValue(TextProperty, value);
        }

        public static readonly BindableProperty CommandProperty =
          BindableProperty.Create(
              nameof(Command),
              typeof(ICommand),
              typeof(DrilldownButton),
              null,
              BindingMode.OneWay);

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(object),
                typeof(DrilldownButton),
                null,
                BindingMode.OneWay);

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public new static readonly BindableProperty IsEnabledProperty =
            BindableProperty.Create(
                nameof(IsEnabled),
                typeof(bool),
                typeof(DrilldownButton),
                true,
                BindingMode.OneWay);

        public new bool IsEnabled
        {
            get => (bool)this.GetValue(IsEnabledProperty);
            set => this.SetValue(IsEnabledProperty, value);
        }

        public static readonly BindableProperty ImageSourceProperty =
         BindableProperty.Create(
             nameof(ImageSource),
             typeof(ImageSource),
             typeof(DrilldownButton),
             null,
             BindingMode.OneWay);

        public ImageSource ImageSource
        {
            get => (ImageSource)this.GetValue(ImageSourceProperty);
            set => this.SetValue(ImageSourceProperty, value);
        }

        public static readonly BindableProperty IsBusyProperty =
            BindableProperty.Create(
                nameof(IsBusy),
                typeof(bool),
                typeof(DrilldownButton),
                false,
                BindingMode.OneWay);

        public bool IsBusy
        {
            get => (bool)this.GetValue(IsBusyProperty);
            set => this.SetValue(IsBusyProperty, value);
        }
    }
}