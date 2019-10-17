using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public interface IColorConfiguration
    {
        /// <summary>
        ///     The underlying color of an app’s content.
        ///     Typically the background color of scrollable content.
        /// </summary>
        Color Background { get; set; }

        /// <summary>
        ///     The color used to indicate error status.
        /// </summary>
        Color Error { get; set; }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Background" />.
        /// </summary>
        Color OnBackground { get; set; }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Error" />.
        /// </summary>
        Color OnError { get; set; }

        /// <summary>
        ///     The color used as background for error callouts, message boxes, error signs.
        /// </summary>
        Color ErrorBackground { get; set; }

        /// <summary>
        ///     Displayed most frequently across your app.
        /// </summary>
        Color Primary { get; set; }

        /// <summary>
        ///     Displayed most frequently across your app.
        /// </summary>
        Color TextColor { get; set; }

        /// <summary>
        ///     Displayed most frequently across your app.
        /// </summary>
        Color TextColorBright { get; set; }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Primary" />.
        /// </summary>
        Color OnPrimary { get; set; }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Secondary" />.
        /// </summary>
        Color OnSecondary { get; set; }

        /// <summary>
        ///     A color that passes accessibility guidelines for text/iconography when drawn on top of <see cref="Surface" />
        /// </summary>
        Color OnSurface { get; set; }

        /// <summary>
        ///     A tonal variation of <see cref="Primary" />.
        /// </summary>
        Color PrimaryVariant { get; set; }

        /// <summary>
        ///     Accents select parts of your UI.
        ///     If not provided, use <see cref="Primary" />.
        /// </summary>
        Color Secondary { get; set; }

        /// <summary>
        ///     A tonal variation of <see cref="Secondary" />.
        /// </summary>
        Color SecondaryVariant { get; set; }

        /// <summary>
        ///     The color of surfaces such as cards, sheets, menus.
        /// </summary>
        Color Surface { get; set; }

        Color SemiTransparentBright { get; }

        Color SemiTransparentDark { get; }

        Color CardViewHeaderTextColor { get; }

        Color CardViewHeaderBackgroundColor { get; }

        Color CardViewDividerColor { get; }

        Color CardViewBackgroundColor { get; }
    }
}