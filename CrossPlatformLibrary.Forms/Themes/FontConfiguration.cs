using System.Collections.Generic;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    ///     Class that provides font configuration based on https://material.io/design/typography.
    /// </summary>
    public sealed class FontConfiguration : BindableObject, IFontConfiguration
    {
        public static readonly BindableProperty FontSizesProperty =
            BindableProperty.Create(
                nameof(FontSizes),
                typeof(ICollection<FontSize>),
                typeof(FontConfiguration),
                CreateDefaultFontSizes());

        public static readonly BindableProperty Body1Property =
            BindableProperty.Create(
                nameof(Body1),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        private static FontElement CreateDefaultBody1()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return new FontElement { FontSize = 17 };
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                return new FontElement { FontSize = 17 };
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                return new FontElement { FontSize = 17 };
            }

            return new FontElement { FontSize = 17 };
        }

        /// <summary>
        ///     Body 1 font family, used for long-form writing and small text sizes.
        /// </summary>
        public FontElement Body1
        {
            get => (FontElement)this.GetValue(Body1Property);
            set => this.SetValue(Body1Property, value);
        }

        public static readonly BindableProperty Body2Property =
            BindableProperty.Create(
                nameof(Body2),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Body 2 font family, used for long-form writing and small text sizes.
        /// </summary>
        public FontElement Body2
        {
            get => (FontElement)this.GetValue(Body2Property);
            set => this.SetValue(Body2Property, value);
        }

        public static readonly BindableProperty ButtonProperty =
            BindableProperty.Create(
                nameof(Button),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        private static FontElement CreateDefaultFontElement()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return new FontElement { FontSize = 17 };
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                return new FontElement { FontSize = 14 };
            }
            
            if (Device.RuntimePlatform == Device.UWP)
            {
                return new FontElement { FontSize = 14 };
            }

            return new FontElement { FontSize = 16 };
        }

        public FontElement Button
        {
            get => (FontElement)this.GetValue(ButtonProperty);
            set => this.SetValue(ButtonProperty, value);
        }

        /// <summary>
        ///     Caption font family, used for annotations or to introduce a headline text.
        /// </summary>
        public static readonly BindableProperty CaptionProperty =
            BindableProperty.Create(
                nameof(Caption),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        public FontElement Caption
        {
            get => (FontElement)this.GetValue(CaptionProperty);
            set => this.SetValue(CaptionProperty, value);
        }

        public static readonly BindableProperty H1Property =
            BindableProperty.Create(
                nameof(H1),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Headline 1 font family, used by large text on the screen.
        /// </summary>
        public FontElement H1
        {
            get => (FontElement)this.GetValue(H1Property);
            set => this.SetValue(H1Property, value);
        }

        public static readonly BindableProperty H2Property =
            BindableProperty.Create(
                nameof(H2),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Headline 2 font family, used by large text on the screen.
        /// </summary>
        public FontElement H2
        {
            get => (FontElement)this.GetValue(H2Property);
            set => this.SetValue(H2Property, value);
        }

        public static readonly BindableProperty H3Property =
            BindableProperty.Create(
                nameof(H3),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Headline 3 font family, used by large text on the screen.
        /// </summary>
        public FontElement H3
        {
            get => (FontElement)this.GetValue(H3Property);
            set => this.SetValue(H3Property, value);
        }

        public static readonly BindableProperty H4Property =
            BindableProperty.Create(
                nameof(H4),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Headline 4 font family, used by large text on the screen.
        /// </summary>
        public FontElement H4
        {
            get => (FontElement)this.GetValue(H4Property);
            set => this.SetValue(H4Property, value);
        }

        public static readonly BindableProperty H5Property =
            BindableProperty.Create(
                nameof(H5),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Headline 5 font family, used by large text on the screen.
        /// </summary>
        public FontElement H5
        {
            get => (FontElement)this.GetValue(H5Property);
            set => this.SetValue(H5Property, value);
        }

        public static readonly BindableProperty H6Property =
            BindableProperty.Create(
                nameof(H6),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Headline 6 font family, used by large text on the screen.
        /// </summary>
        public FontElement H6
        {
            get => (FontElement)this.GetValue(H6Property);
            set => this.SetValue(H6Property, value);
        }

        public static readonly BindableProperty OverlineProperty =
            BindableProperty.Create(
                nameof(Overline),
                typeof(FontElement),
                typeof(FontConfiguration),
                CreateDefaultOverline());

        private static FontElement CreateDefaultOverline()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return new FontElement { FontSize = 12 };
            }

            if (Device.RuntimePlatform == Device.Android)
            {
                return new FontElement { FontSize = 12 };
            }

            if (Device.RuntimePlatform == Device.UWP)
            {
                return new FontElement { FontSize = 12 };
            }

            return new FontElement { FontSize = 12 };
        }

        /// <summary>
        ///     Overline font family, used for annotations or to introduce a headline text.
        /// </summary>
        public FontElement Overline
        {
            get => (FontElement)this.GetValue(OverlineProperty);
            set => this.SetValue(OverlineProperty, value);
        }

        public static readonly BindableProperty Subtitle1Property =
            BindableProperty.Create(
                nameof(Subtitle1),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Subtitle 1 font family, used by medium-emphasis text.
        /// </summary>
        public FontElement Subtitle1
        {
            get => (FontElement)this.GetValue(Subtitle1Property);
            set => this.SetValue(Subtitle1Property, value);
        }

        public static readonly BindableProperty Subtitle2Property =
            BindableProperty.Create(
                nameof(Subtitle2),
                typeof(FontElement),
                typeof(FontConfiguration),
                null);

        /// <summary>
        ///     Subtitle 2 font family, used by medium-emphasis text.
        /// </summary>
        public FontElement Subtitle2
        {
            get => (FontElement)this.GetValue(Subtitle2Property);
            set => this.SetValue(Subtitle2Property, value);
        }

        public ICollection<FontSize> FontSizes
        {
            get => (ICollection<FontSize>)this.GetValue(FontSizesProperty);
            set => this.SetValue(FontSizesProperty, value);
        }

        private static ICollection<FontSize> CreateDefaultFontSizes()
        {
            if (Device.RuntimePlatform == Device.iOS)
            {
                return new List<FontSize>
                {
                    //new FontSize { Key = ThemeConstants.FontSizes.Body1, Value = 16 }
                };
            }

            return new List<FontSize>
            {
                //new FontSize { Key = ThemeConstants.FontSizes.Default, Value = 14 }
            };
        }
    }
}