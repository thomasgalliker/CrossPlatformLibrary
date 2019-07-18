using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public partial class DrilldownSwitchCell : ExtendedViewCell
    {
        public DrilldownSwitchCell()
        {
            try
            {
                this.InitializeComponent();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                throw;
            }
        }

        public static readonly BindableProperty TextProperty =
            BindableProperty.Create(
                nameof(Text),
                typeof(string),
                typeof(DrilldownSwitchCell),
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
                typeof(DrilldownSwitchCell),
                null,
                BindingMode.OneWay);

        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(object),
                typeof(DrilldownSwitchCell),
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
                typeof(DrilldownSwitchCell),
                true,
                BindingMode.OneWay);

        public new bool IsEnabled
        {
            get { return (bool)this.GetValue(IsEnabledProperty); }
            set { this.SetValue(IsEnabledProperty, value); }
        }

        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(nameof(IsToggled),
                typeof(bool),
                typeof(DrilldownSwitchCell),
                false,
                BindingMode.TwoWay);

        public bool IsToggled
        {
            get => (bool)this.GetValue(IsToggledProperty);
            set => this.SetValue(IsToggledProperty, (object)value);
        }
    }
}