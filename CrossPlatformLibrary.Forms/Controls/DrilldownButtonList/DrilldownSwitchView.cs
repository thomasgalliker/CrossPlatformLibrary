using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class DrilldownSwitchView : DrilldownView, IDrilldownSwitchView
    {
        public static readonly BindableProperty IsToggledProperty =
            BindableProperty.Create(nameof(IsToggled),
                typeof(bool),
                typeof(DrilldownSwitchView),
                false,
                BindingMode.TwoWay);

        public bool IsToggled
        {
            get => (bool)this.GetValue(IsToggledProperty);
            set => this.SetValue(IsToggledProperty, value);
        }
    }
}