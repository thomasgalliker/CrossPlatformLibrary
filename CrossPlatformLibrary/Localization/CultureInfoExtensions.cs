using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace CrossPlatformLibrary.Localization
{
    public static class CultureInfoExtensions
    {
        public static CultureInfo ToCultureInfo(this string language, CultureInfo defaultCultureInfo)
        {
            if (language == null)
            {
                return defaultCultureInfo;
            }

            try
            {
                return new CultureInfo(language);
            }
            catch
            {
                // ignored
            }

            return defaultCultureInfo;
        }

        public static CultureInfo GetBestMatch(this IEnumerable<CultureInfo> cultureInfos, string language, CultureInfo defaultCultureInfo)
        {
            var source = language.ToCultureInfo(defaultCultureInfo);
            return cultureInfos.GetBestMatch(source, defaultCultureInfo);
        }

        public static CultureInfo GetBestMatch(this IEnumerable<CultureInfo> cultureInfos, CultureInfo source, CultureInfo defaultCultureInfo)
        {

            var cultureInfo = cultureInfos.FirstOrDefault(c => string.Equals(c.Name, source.Name, StringComparison.OrdinalIgnoreCase) || string.Equals(c.TwoLetterISOLanguageName, source.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase));
            if (cultureInfo == null)
            {
                // If the user's configured language is not available,
                // we use the default language as fallback
                return defaultCultureInfo;
            }

            return cultureInfo;
        }
    }
}