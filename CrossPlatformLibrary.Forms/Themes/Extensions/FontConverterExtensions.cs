using CrossPlatformLibrary.Forms.Services;

namespace CrossPlatformLibrary.Forms.Themes.Extensions
{
    internal static class FontConverterExtensions
    {
        internal static IFontSizeConfiguration GetScaledFontSizes(this IFontConverter fontConverter, IFontSizeConfiguration fontSizeConfiguration)
        {
            return new FontSizeConfiguration
            {
                Micro = fontConverter.GetScaledFontSize(fontSizeConfiguration.Micro),
                XSmall = fontConverter.GetScaledFontSize(fontSizeConfiguration.XSmall),
                Small = fontConverter.GetScaledFontSize(fontSizeConfiguration.Small),
                MidMedium = fontConverter.GetScaledFontSize(fontSizeConfiguration.MidMedium),
                Medium = fontConverter.GetScaledFontSize(fontSizeConfiguration.Medium),
                Large = fontConverter.GetScaledFontSize(fontSizeConfiguration.Large),
                XLarge = fontConverter.GetScaledFontSize(fontSizeConfiguration.XLarge),
                XXLarge = fontConverter.GetScaledFontSize(fontSizeConfiguration.XXLarge),
                XXXLarge = fontConverter.GetScaledFontSize(fontSizeConfiguration.XXXLarge),
            };
        }
    }
}