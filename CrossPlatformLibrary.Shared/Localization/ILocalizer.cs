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
        ///     Returns platform-specific locale settings.
        /// </summary>
        CultureInfo GetCurrentCulture();

        /// <summary>
        ///     Sets all relevant culture settings to <paramref name="cultureInfo" />.
        /// </summary>
        /// <remarks>
        ///     This method must be run from the UI thread.
        /// </remarks>
        void SetCultureInfo(CultureInfo cultureInfo);

        /// <summary>
        ///     Event is raised when the current culture info has changed.
        /// </summary>
        event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;
    }
}