using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomLabel : Label
    {
        public const int DefaultLinesValue = -1;

        public static readonly BindableProperty LinesProperty =
            BindableProperty.Create(
                nameof(Lines),
                typeof(int),
                typeof(CustomLabel),
                DefaultLinesValue);

        public int Lines
        {
            get => (int)this.GetValue(LinesProperty);
            set => this.SetValue(LinesProperty, value);
        }

        public static readonly BindableProperty JustifyTextProperty =
            BindableProperty.Create(
                nameof(JustifyText),
                typeof(bool),
                typeof(CustomLabel),
                false,
                BindingMode.OneWay
            );

        public bool JustifyText
        {
            get => (bool)this.GetValue(JustifyTextProperty);
            set => this.SetValue(JustifyTextProperty, value);
        }

        public static readonly BindableProperty RemovePaddingProperty =
            BindableProperty.Create(
                nameof(RemovePadding),
                typeof(bool),
                typeof(CustomLabel),
                false,
                BindingMode.OneWay
            );

        public bool RemovePadding
        {
            get => (bool)this.GetValue(RemovePaddingProperty);
            set => this.SetValue(RemovePaddingProperty, value);
        }
    }
}