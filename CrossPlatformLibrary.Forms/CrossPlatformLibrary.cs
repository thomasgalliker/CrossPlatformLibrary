using System;
using CrossPlatformLibrary.Forms.Resources;
using CrossPlatformLibrary.Forms.Themes;
using CrossPlatformLibrary.Forms.Themes.Extensions;
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
            this.config = config ?? GetDefaultConfiguration();
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
                ColorConfiguration = new ColorConfiguration(),
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
            this.applicationResources.MergedDictionaries.Add(new ThemeColorResources(this.config.ColorConfiguration ?? new ColorConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new ThemeSpacingResources(this.config.SpacingConfiguration ?? new SpacingConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new ThemeFontResources(this.config.FontConfiguration ?? new FontConfiguration()));
        }


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