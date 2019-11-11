using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public class FontElement : BindableObject
    {
        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create(
                nameof(FontFamily),
                typeof(string),
                typeof(FontElement),
                null);

        public string FontFamily
        {
            get => (string)this.GetValue(FontFamilyProperty);
            set => this.SetValue(FontFamilyProperty, value);
        }

        public static readonly BindableProperty FontSizeProperty =
            BindableProperty.Create(
                nameof(FontSize),
                typeof(double),
                typeof(FontElement),
                0d);

        [TypeConverter(typeof(FontSizeConverter))]
        public double FontSize
        {
            get => (double)this.GetValue(FontSizeProperty);
            set => this.SetValue(FontSizeProperty, value);
        }

        public static readonly BindableProperty FontAttributesProperty =
            BindableProperty.Create(
                nameof(FontAttributes),
                typeof(FontAttributes),
                typeof(FontElement),
                FontAttributes.None);

        public FontAttributes FontAttributes
        {
            get => (FontAttributes)this.GetValue(FontAttributesProperty);
            set => this.SetValue(FontAttributesProperty, value);
        }
    }
}