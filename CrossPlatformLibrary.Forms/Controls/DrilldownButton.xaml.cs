using System.Windows.Input;
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
            get { return (string)this.GetValue(TextProperty); }
            set { this.SetValue(TextProperty, value); }
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
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
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
            get { return (bool)this.GetValue(IsEnabledProperty); }
            set { this.SetValue(IsEnabledProperty, value); }
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
            get { return (ImageSource)this.GetValue(ImageSourceProperty); }
            set { this.SetValue(ImageSourceProperty, value); }
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
            get { return (bool)this.GetValue(IsBusyProperty); }
            set { this.SetValue(IsBusyProperty, value); }
        }
    }
}