using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class GradientStackLayout : StackLayout
    {
        public static readonly BindableProperty StartColorProperty =
            BindableProperty.Create(nameof(StartColor),
                typeof(Color),
                typeof(GradientStackLayout),
                Color.Accent);

        public static readonly BindableProperty EndColorProperty =
            BindableProperty.Create(nameof(EndColor),
                typeof(Color),
                typeof(GradientStackLayout),
                Color.Accent);

        public Color StartColor
        {
            get => (Color)this.GetValue(StartColorProperty);
            set => this.SetValue(StartColorProperty, value);
        }

        public Color EndColor
        {
            get => (Color)this.GetValue(EndColorProperty);
            set => this.SetValue(EndColorProperty, value);
        }
    }
}