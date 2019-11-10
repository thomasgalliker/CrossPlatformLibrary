namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    /// Named font sizes.
    /// Source: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
    /// </summary>
    public interface IFontSizeConfiguration
    {
        double Micro { get; set; }

        double Small { get; set; }

        double Medium { get; set; }

        double Large { get; set; }
    }
}