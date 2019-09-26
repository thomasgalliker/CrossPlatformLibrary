using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomEntry : Entry
    {
        private static Thickness DefaultPadding = default(Thickness);
        private static Thickness AndroidPadding = new Thickness(left: 12, top: 30, right: 12, bottom: 33);

        public static readonly BindableProperty PaddingProperty =
            BindableProperty.Create(
                nameof(Padding),
                typeof(Thickness),
                typeof(CustomEntry),
                GetPlatformDefaultPadding());

        private static Thickness GetPlatformDefaultPadding()
        {
            if (Device.RuntimePlatform == Device.Android)
            {
                return AndroidPadding;
            }

            return DefaultPadding;
        }

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

        public static readonly BindableProperty TextContentTypeProperty =
            BindableProperty.Create(
                nameof(TextContentType),
                typeof(TextContentType),
                typeof(CustomEntry),
                TextContentType.Default,
                BindingMode.OneWay);

        public TextContentType TextContentType
        {
            get => (TextContentType)this.GetValue(TextContentTypeProperty);
            set => this.SetValue(TextContentTypeProperty, value);
        }
    }
}