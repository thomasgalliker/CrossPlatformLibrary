using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class CustomEntry : Entry
    {
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

        public static readonly BindableProperty RemovePaddingProperty =
            BindableProperty.Create(
                nameof(RemovePadding),
                typeof(bool),
                typeof(CustomEntry),
                false,
                BindingMode.OneWay
            );

        public bool RemovePadding
        {
            get => (bool)this.GetValue(RemovePaddingProperty);
            set => this.SetValue(RemovePaddingProperty, value);
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