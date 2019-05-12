using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomLabel : Label
    {
        private static readonly int defaultLineSetting = -1;

        public static readonly BindableProperty LinesProperty =
            BindableProperty.Create(
                nameof(Lines),
                typeof(int),
                typeof(CustomLabel),
                defaultLineSetting);

        public int Lines
        {
            get { return (int)this.GetValue(LinesProperty); }
            set { this.SetValue(LinesProperty, value); }
        }
    }
}