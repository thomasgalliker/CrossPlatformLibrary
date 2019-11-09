using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    ///     Class that provides font configuration based on https://material.io/design/typography.
    /// </summary>
    public sealed class FontConfiguration : BindableObject, IFontConfiguration
    {
        public static readonly BindableProperty DefaultProperty =
            BindableProperty.Create(
                nameof(Default),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        public FontElement Default
        {
            get => (FontElement)this.GetValue(DefaultProperty);
            set => this.SetValue(DefaultProperty, value);
        }

        public static readonly BindableProperty ButtonProperty =
            BindableProperty.Create(
                nameof(Button),
                typeof(FontElement),
                typeof(FontConfiguration),
                FontElement.Default);

        public FontElement Button
        {
            get => (FontElement)this.GetValue(ButtonProperty);
            set => this.SetValue(ButtonProperty, value);
        }

        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(FontElement),
                typeof(FontConfiguration),
                FontElement.Default);

        public FontElement Title
        {
            get => (FontElement)this.GetValue(TitleProperty);
            set => this.SetValue(TitleProperty, value);
        }
    }
}