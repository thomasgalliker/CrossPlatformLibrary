using System;
using CrossPlatformLibrary.Forms.Resources;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms
{
    /// <summary>
    ///     Class that provides static methods and properties for configuring CrossPlatformLibrary resources.
    /// </summary>
    public class CrossPlatformLibrary
    {
        private readonly ITheme config;
        private readonly ResourceDictionary applicationResources;

        internal CrossPlatformLibrary(Application app, ITheme config)
        {
            this.applicationResources = app.Resources;
            this.config = config ?? GetDefaultConfiguration(); ;
        }

        internal CrossPlatformLibrary(Application app, string key)
        {
            this.applicationResources = app.Resources;
            this.config = app.Resources.ResolveTheme<ITheme>(key);
        }

        internal CrossPlatformLibrary(Application app)
        {
            this.applicationResources = app.Resources;
            this.config = GetDefaultConfiguration();
        }

        private static CrossPlatformLibraryConfiguration GetDefaultConfiguration()
        {
            return new CrossPlatformLibraryConfiguration
            {
                ColorConfiguration = new CrossPlatformLibraryColorConfiguration(),
            };
        }

        /// <summary>
        ///     Configures the current app's resources by merging pre-defined CrossPlatformLibrary resources and creating new
        ///     resources based on the <see cref="CrossPlatformLibraryConfiguration" />'s properties.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="crossPlatformLibraryResource">The configuration.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, ITheme crossPlatformLibraryResource)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (crossPlatformLibraryResource == null)
            {
                throw new ArgumentNullException(nameof(crossPlatformLibraryResource));
            }

            var crossPlatformLibrary = new CrossPlatformLibrary(app, crossPlatformLibraryResource);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
        }

        /// <summary>
        ///     Configures the current app's resources by merging pre-defined CrossPlatformLibrary resources and creating new
        ///     resources based on the <see cref="ITheme" />'s properties.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="key">
        ///     The key of the <see cref="ITheme" /> object in the current app's resource
        ///     dictionary.
        /// </param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, string key)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var crossPlatformLibrary = new CrossPlatformLibrary(app, key);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
        }

        /// <summary>
        ///     Configures the current app's resources by merging pre-defined CrossPlatformLibrary resources.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            var crossPlatformLibrary = new CrossPlatformLibrary(app);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
        }

        private void MergeCrossPlatformLibraryDictionaries()
        {
            this.applicationResources.MergedDictionaries.Add(new ThemeColorResources(this.config.ColorConfiguration ?? new CrossPlatformLibraryColorConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new MaterialSizes());
        }

        /// <summary>
        ///     Static class that contains the current CrossPlatformLibrary color values.
        /// </summary>
        //public static class Color
        //{
        //    /// <summary>
        //    ///     The underlying color of an app’s content.
        //    ///     Typically the background color of scrollable content.
        //    /// </summary>
        //    public static Xamarin.Forms.Color Background => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.BACKGROUND);

        //    /// <summary>
        //    ///     The color used to indicate error status.
        //    /// </summary>
        //    public static Xamarin.Forms.Color Error => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.ERROR);

        //    /// <summary>
        //    ///     A color that passes accessibility guidelines for text/iconography when drawn on top of
        //    ///     <see cref="CrossPlatformLibraryColorConfiguration.Background" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color OnBackground => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.ON_BACKGROUND);

        //    /// <summary>
        //    ///     A color that passes accessibility guidelines for text/iconography when drawn on top of
        //    ///     <see cref="CrossPlatformLibraryColorConfiguration.Error" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color OnError => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.ON_ERROR);

        //    /// <summary>
        //    ///     A color that passes accessibility guidelines for text/iconography when drawn on top of
        //    ///     <see cref="CrossPlatformLibraryColorConfiguration.Primary" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color OnPrimary => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.ON_PRIMARY);

        //    /// <summary>
        //    ///     A color that passes accessibility guidelines for text/iconography when drawn on top of
        //    ///     <see cref="CrossPlatformLibraryColorConfiguration.Secondary" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color OnSecondary => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.ON_SECONDARY);

        //    /// <summary>
        //    ///     A color that passes accessibility guidelines for text/iconography when drawn on top of
        //    ///     <see cref="CrossPlatformLibraryColorConfiguration.Surface" />
        //    /// </summary>
        //    public static Xamarin.Forms.Color OnSurface => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.ON_SURFACE);

        //    /// <summary>
        //    ///     Displayed most frequently across your app.
        //    /// </summary>
        //    public static Xamarin.Forms.Color Primary => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.PRIMARY);

        //    /// <summary>
        //    ///     A tonal variation of <see cref="CrossPlatformLibraryColorConfiguration.Primary" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color PrimaryVariant => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.PRIMARY_VARIANT);

        //    /// <summary>
        //    ///     Accents select parts of your UI.
        //    ///     If not provided, use <see cref="CrossPlatformLibraryColorConfiguration.Primary" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color Secondary => GetSecondaryColor();

        //    //TODO: Make configurations bindable.
        //    private static Xamarin.Forms.Color GetSecondaryColor()
        //    {
        //        var color = GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.SECONDARY);

        //        return color.IsDefault ? Xamarin.Forms.Color.FromHex("#6200EE") : color;
        //    }

        //    /// <summary>
        //    ///     A tonal variation of <see cref="CrossPlatformLibraryColorConfiguration.Secondary" />.
        //    /// </summary>
        //    public static Xamarin.Forms.Color SecondaryVariant => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.SECONDARY_VARIANT);

        //    /// <summary>
        //    ///     The color of surfaces such as cards, sheets, menus.
        //    /// </summary>
        //    public static Xamarin.Forms.Color Surface => GetResource<Xamarin.Forms.Color>(ThemeConstants.Color.SURFACE);
        //}

        /// <summary>
        ///     Static class that contains the current CrossPlatformLibrary font family values.
        /// </summary>
        //public static class FontFamily
        //{
        //    /// <summary>
        //    ///     Body 1 font family, used for long-form writing and small text sizes.
        //    /// </summary>
        //    public static string Body1 => GetResource<string>(ThemeConstants.FontFamily.BODY1);

        //    /// <summary>
        //    ///     Body 2 font family, used for long-form writing and small text sizes.
        //    /// </summary>
        //    public static string Body2 => GetResource<string>(ThemeConstants.FontFamily.BODY2);

        //    /// <summary>
        //    ///     Button font family, used by different types of buttons.
        //    /// </summary>
        //    public static string Button => GetResource<string>(ThemeConstants.FontFamily.BUTTON);

        //    /// <summary>
        //    ///     Caption font family, used for annotations or to introduce a headline text.
        //    /// </summary>
        //    public static string Caption => GetResource<string>(ThemeConstants.FontFamily.CAPTION);

        //    /// <summary>
        //    ///     Headline 1 font family, used by large text on the screen.
        //    /// </summary>
        //    public static string H1 => GetResource<string>(ThemeConstants.FontFamily.H1);

        //    /// <summary>
        //    ///     Headline 2 font family, used by large text on the screen.
        //    /// </summary>
        //    public static string H2 => GetResource<string>(ThemeConstants.FontFamily.H2);

        //    /// <summary>
        //    ///     Headline 3 font family, used by large text on the screen.
        //    /// </summary>
        //    public static string H3 => GetResource<string>(ThemeConstants.FontFamily.H3);

        //    /// <summary>
        //    ///     Headline 4 font family, used by large text on the screen.
        //    /// </summary>
        //    public static string H4 => GetResource<string>(ThemeConstants.FontFamily.H4);

        //    /// <summary>
        //    ///     Headline 5 font family, used by large text on the screen.
        //    /// </summary>
        //    public static string H5 => GetResource<string>(ThemeConstants.FontFamily.H5);

        //    /// <summary>
        //    ///     Headline 6 font family, used by large text on the screen.
        //    /// </summary>
        //    public static string H6 => GetResource<string>(ThemeConstants.FontFamily.H6);

        //    /// <summary>
        //    ///     Overline font family, used for annotations or to introduce a headline text.
        //    /// </summary>
        //    public static string Overline => GetResource<string>(ThemeConstants.FontFamily.OVERLINE);

        //    /// <summary>
        //    ///     Subtitle 1 font family, used by medium-emphasis text.
        //    /// </summary>
        //    public static string Subtitle1 => GetResource<string>(ThemeConstants.FontFamily.SUBTITLE1);

        //    /// <summary>
        //    ///     Subtitle 2 font family, used by medium-emphasis text.
        //    /// </summary>
        //    public static string Subtitle2 => GetResource<string>(ThemeConstants.FontFamily.SUBTITLE2);
        //}
    }
}