using System;
using System.Diagnostics;
using CrossPlatformLibrary.Forms.Resources;
using CrossPlatformLibrary.Forms.Services;
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
        private static IFontConverter fontConverter = new NullFontConverter();

        private readonly ITheme config;
        private readonly ResourceDictionary applicationResources;

        private CrossPlatformLibrary(Application app, ITheme config)
        {
            this.applicationResources = app.Resources;
            this.config = config ?? GetDefaultConfiguration();
        }

        private static CrossPlatformLibraryConfiguration GetDefaultConfiguration()
        {
            return new CrossPlatformLibraryConfiguration
            {
                ColorConfiguration = new ColorConfiguration(),
            };
        }

        /// <summary>
        /// Sets the <seealso cref="IFontConverter"/> which is used to scale font sizes.
        /// </summary>
        /// <param name="converter"></param>
        public static void SetFontConverter(IFontConverter converter)
        {
            fontConverter = converter ?? throw new ArgumentNullException(nameof(converter));
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

            Init(app, GetDefaultConfiguration());
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

            var stopwatch = Stopwatch.StartNew();
            var theme = app.Resources.ResolveTheme<ITheme>(key);
            Init(app, theme);
            Debug.WriteLine($"Init with key='{key}' finished in {stopwatch.Elapsed.TotalMilliseconds:F0}ms");
        }

        /// <summary>
        ///     Configures the current app's resources by merging pre-defined CrossPlatformLibrary resources and creating new
        ///     resources based on the <see cref="CrossPlatformLibraryConfiguration" />'s properties.
        /// </summary>
        /// <param name="app">The cross-platform mobile application that is running.</param>
        /// <param name="theme">The configuration.</param>
        /// <exception cref="ArgumentNullException" />
        public static void Init(Application app, ITheme theme)
        {
            if (app == null)
            {
                throw new ArgumentNullException(nameof(app));
            }

            if (theme == null)
            {
                throw new ArgumentNullException(nameof(theme));
            }

            var stopwatch = Stopwatch.StartNew();
            var crossPlatformLibrary = new CrossPlatformLibrary(app, theme);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
            Debug.WriteLine($"Init finished in {stopwatch.Elapsed.TotalMilliseconds:F0}ms");
        }

        private void MergeCrossPlatformLibraryDictionaries()
        {
            this.applicationResources.MergedDictionaries.Add(new ThemeColorResources(this.config.ColorConfiguration ?? new ColorConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new ThemeSpacingResources(this.config.SpacingConfiguration ?? new SpacingConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new ThemeFontResources(this.config.FontConfiguration ?? new FontConfiguration(), fontConverter));
        }
    }
}