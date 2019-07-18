using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomLabel : Label
    {
        public static readonly BindableProperty LinesProperty =
            BindableProperty.Create(
                nameof(Lines),
                typeof(int),
                typeof(CustomLabel),
                -1);

        public int Lines
        {
            get => (int)this.GetValue(LinesProperty);
            set => this.SetValue(LinesProperty, value);
        }
    }
}