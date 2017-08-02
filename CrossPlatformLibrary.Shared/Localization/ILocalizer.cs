using System;
using System.Globalization;

namespace CrossPlatformLibrary.Localization
{
    /// <summary>
    ///     Implementations of this interface MUST convert iOS and Android
    ///     platform-specific locales to a value supported in .NET because
    ///     ONLY valid .NET cultures can have their RESX resources loaded and used.
    /// </summary>
    /// <remarks>
    ///     Lists of valid .NET cultures can be found here:
    ///     http://www.localeplanet.com/dotnet/
    ///     http://www.csharp-examples.net/culture-names/
    /// </remarks>
    public interface ILocalizer
    {
        /// <summary>
        ///     This method must evaluate platform-specific locale settings
        ///     and convert them (when necessary) to a valid .NET locale.
        /// </summary>
        CultureInfo GetCurrentCulture();

        /// <summary>
        ///     CurrentCulture and CurrentUICulture must be set in the platform project,
        ///     because the Thread object can't be accessed in a PCL.
        /// </summary>
        void SetCultureInfo(CultureInfo cultureInfo);

        /// <summary>
        ///     Event is raised when the current culture info has changed.
        /// </summary>
        event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;
    }
}