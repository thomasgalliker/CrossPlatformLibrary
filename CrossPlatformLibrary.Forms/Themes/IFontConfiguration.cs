namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    ///     Font configuration with named fonts, based on ideas behind https://material.io/design/typography.
    /// </summary>
    public interface IFontConfiguration
    {
        IFontSizeConfiguration FontSizes { get; set; }

        FontElement Default { get; set; }

        FontElement SectionLabel { get; set; }

        FontElement Body1 { get; set; }

        FontElement Body2 { get; set; }

        FontElement Button { get; set; }

        FontElement Input { get; set; }

        FontElement Caption { get; set; }

        FontElement H1 { get; set; }

        FontElement H2 { get; set; }

        FontElement H3 { get; set; }

        FontElement H4 { get; set; }

        FontElement H5 { get; set; }

        FontElement H6 { get; set; }

        FontElement Overline { get; set; }

        FontElement Title { get; set; }

        FontElement Subtitle1 { get; set; }

        FontElement Subtitle2 { get; set; }
    }

    public class FontSize
    {
        public string Key { get; set; }

        public double Value { get; set; }
    }
}