using System;
using System.Diagnostics;
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

            var stopwatch = Stopwatch.StartNew();
            var crossPlatformLibrary = new CrossPlatformLibrary(app, crossPlatformLibraryResource);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
            Debug.WriteLine($"Init finished in {stopwatch.Elapsed.TotalMilliseconds:F0}ms");
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
            var crossPlatformLibrary = new CrossPlatformLibrary(app, key);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
            Debug.WriteLine($"Init finished in {stopwatch.Elapsed.TotalMilliseconds:F0}ms");
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

            var stopwatch = Stopwatch.StartNew();
            var crossPlatformLibrary = new CrossPlatformLibrary(app);
            crossPlatformLibrary.MergeCrossPlatformLibraryDictionaries();
            Debug.WriteLine($"Init finished in {stopwatch.Elapsed.TotalMilliseconds:F0}ms");
        }

        private void MergeCrossPlatformLibraryDictionaries()
        {
            this.applicationResources.MergedDictionaries.Add(new ThemeColorResources(this.config.ColorConfiguration ?? new ColorConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new ThemeSpacingResources(this.config.SpacingConfiguration ?? new SpacingConfiguration()));
            this.applicationResources.MergedDictionaries.Add(new ThemeFontResources(this.config.FontConfiguration ?? new FontConfiguration()));
        }
    }
}