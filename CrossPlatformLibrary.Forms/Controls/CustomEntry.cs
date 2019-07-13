using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomEntry : Entry
    {
        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(
                nameof(Padding),
                typeof(Thickness),
                typeof(CustomEntry),
                new Thickness());

        public Thickness Padding
        {
            get => (Thickness)this.GetValue(PaddingProperty);
            set => this.SetValue(PaddingProperty, value);
        }

        public static readonly BindableProperty HideBorderProperty =
            BindableProperty.Create(
                nameof(HideBorder),
                typeof(bool),
                typeof(CustomEntry),
                default(bool),
                BindingMode.OneWay);

        public bool HideBorder
        {
            get => (bool)this.GetValue(HideBorderProperty);
            set => this.SetValue(HideBorderProperty, value);
        }
    }
}