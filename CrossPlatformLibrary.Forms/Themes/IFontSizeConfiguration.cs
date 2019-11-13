namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    /// Global definition for named font sizes.
    /// You can adjust particular font sizes to the current application's needs.
    /// 
    /// Source: https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/text/fonts
    /// </summary>
    public interface IFontSizeConfiguration
    {
        double Micro { get; set; }

        double XSmall { get; set; }

        double Small { get; set; }

        double MidMedium { get; set; }

        double Medium { get; set; }

        double Large { get; set; }

        double XLarge { get; set; }

        double XXLarge { get; set; }

        double XXXLarge { get; set; }
    }
}