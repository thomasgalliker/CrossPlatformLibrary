namespace CrossPlatformLibrary.Forms.Themes
{
    public interface IFontConfiguration
    {
        /// <summary>
        ///     Default font family, used for long-form writing and small text sizes.
        /// </summary>
        FontElement Default { get; set; }

        /// <summary>
        ///     Button font family, used by different types of buttons.
        /// </summary>
        FontElement Button { get; set; }

        /// <summary>
        ///     Caption font family, used for annotations or to introduce a headline text.
        /// </summary>
        FontElement Title { get; set; }
    }
}