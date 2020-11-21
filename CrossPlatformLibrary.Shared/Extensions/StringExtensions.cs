using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CrossPlatformLibrary.Extensions
{
    public static class StringExtensions
    {
        #region Static Fields

        private static readonly Dictionary<char, char> DiacriticsMappings;

        private static readonly char[] EnglishAccents = { 'é' };

        private static readonly char[] EnglishReplace = { 'e' };

        private static readonly char[] ArabicAccents = { 'أ', 'إ', 'آ', 'ء', 'پ', 'ض', 'ذ', 'ـ', 'خ', 'خ', 'غ', 'ش', 'ة', 'ث', 'ً', 'ٰ', 'ؤ', 'ظ', 'ى', 'ئ' };

        private static readonly char[] ArabicReplace = { 'ا', 'ا', 'ا', 'ا', 'ب', 'ص', 'د', 'ّ', 'ح', 'ك', 'ع', 'س', 'ت', 'ت', 'َ', 'َ', 'و', 'ط', 'ي', 'ي' };

        private static readonly char[] BulgarianAccents = { 'ъ', 'ѝ' };

        private static readonly char[] BulgarianReplace = { 'ь', 'и' };

        private static readonly char[] CatalanAccents = { 'à', 'è', 'é', 'í', 'ï', 'ò', 'ó', 'ú', 'ü' };

        private static readonly char[] CatalanReplace = { 'a', 'e', 'e', 'i', 'i', 'o', 'o', 'u', 'u' };

        private static readonly char[] CroatianAccents = { 'č', 'ć', 'đ', 'š', 'ž' };

        private static readonly char[] CroatianReplace = { 'c', 'c', 'd', 's', 'z' };

        private static readonly char[] CzechAccents = { 'ã', 'á', 'á', 'č', 'ď', 'é', 'ě', 'í', 'ň', 'ó', 'ř', 'š', 'ť', 'ú', 'ů', 'ý', 'ž' };

        private static readonly char[] CzechReplace = { 'a', 'a', 'a', 'c', 'd', 'e', 'e', 'i', 'n', 'o', 'r', 's', 't', 'u', 'u', 'y', 'z' };

        private static readonly char[] DutchAccents = { 'é', 'ë', 'ï', 'ó', 'ö', 'ü' };

        private static readonly char[] DutchReplace = { 'e', 'e', 'i', 'o', 'o', 'u' };

        private static readonly char[] EstonianAccents = { 'ä', 'ö', 'õ', 'ü' };

        private static readonly char[] EstonianReplace = { 'a', 'o', 'o', 'u' };

        private static readonly char[] FilipinoAccents = { 'á', 'à', 'â', 'é', 'è', 'ê', 'í', 'ì', 'î', 'ó', 'ò', 'ô', 'ú', 'ù', 'û' };

        private static readonly char[] FilipinoReplace = { 'a', 'a', 'a', 'e', 'e', 'e', 'i', 'i', 'i', 'o', 'o', 'o', 'u', 'u', 'u' };

        private static readonly char[] FrenchAccents = { 'à', 'â', 'ä', 'æ', 'ç', 'é', 'è', 'ê', 'ë', 'î', 'ï', 'ô', 'œ', 'ù', 'û', 'ü' };

        private static readonly char[] FrenchReplace = { 'a', 'a', 'a', 'a', 'c', 'e', 'e', 'e', 'e', 'i', 'i', 'o', 'o', 'u', 'u', 'u' };

        private static readonly char[] GermanAccents = { 'ä', 'ö', 'ü', 'ß' };

        private static readonly char[] GermanReplace = { 'a', 'o', 'u', 's' };

        private static readonly char[] GreekAccents = { 'ά', 'έ', 'ή', 'ί', 'ϊ', 'ΐ', 'ό', 'ύ', 'ϋ', 'ΰ', 'ώ' };

        private static readonly char[] GreekReplace = { 'α', 'ε', 'η', 'ι', 'ι', 'ι', 'ο', 'υ', 'υ', 'υ', 'ω' };

        private static readonly char[] HungarianAccents = { 'á', 'é', 'í', 'ö', 'ó', 'ő', 'ü', 'ú', 'ű' };

        private static readonly char[] HungarianReplace = { 'a', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'u' };

        private static readonly char[] IcelandicAccents = { 'ö' };

        private static readonly char[] IcelandicReplace = { 'o' };

        private static readonly char[] ItalianAccents = { 'à', 'è', 'é', 'ì', 'ò', 'ó', 'ù' };

        private static readonly char[] ItalianReplace = { 'a', 'e', 'e', 'i', 'o', 'o', 'u' };

        private static readonly char[] LatvianAccents = { 'ē' };

        private static readonly char[] LatvianReplace = { 'e' };

        private static readonly char[] PolishAccents = { 'ą', 'ć', 'ę', 'ł', 'ń', 'ó', 'ś', 'ż', 'ź' };

        private static readonly char[] PolishReplace = { 'a', 'c', 'e', 'l', 'n', 'o', 's', 'z', 'z' };

        private static readonly char[] PortugueseAccents = { 'ã', 'á', 'â', 'à', 'é', 'ê', 'í', 'õ', 'ó', 'ô', 'ú', 'ü' };

        private static readonly char[] PortugueseReplace = { 'a', 'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u' };

        private static readonly char[] RomanianAccents = { 'ă', 'â', 'î', 'ş', 'ș', 'ţ', 'ț' };

        private static readonly char[] RomanianReplace = { 'a', 'a', 'i', 's', 's', 't', 't' };

        private static readonly char[] RussianAccents = { 'ъ' };

        private static readonly char[] RussianReplace = { 'b' };

        private static readonly char[] SlovakianAccents = { 'á', 'ä', 'č', 'ď', 'é', 'í', 'ĺ', 'ľ', 'ň', 'ó', 'ô', 'ŕ', 'š', 'ť', 'ú', 'ý', 'ž' };

        private static readonly char[] SlovakianReplace = { 'a', 'a', 'c', 'd', 'e', 'i', 'l', 'l', 'n', 'o', 'o', 'r', 's', 't', 'u', 'y', 'z' };

        private static readonly char[] SpanishAccents = { 'á', 'é', 'í', 'ó', 'ú' };

        private static readonly char[] SpanishReplace = { 'a', 'e', 'i', 'o', 'u' };

        private static readonly char[] TurkishAccents = { 'ç', 'é', 'ë', 'ğ', 'İ', 'ï', 'ó', 'ö', 'ü' };

        private static readonly char[] TurkishReplace = { 'c', 'e', 'e', 'g', 'i', 'i', 'o', 'o', 'u' };

        private static readonly char[] UkarainianAccents = { 'ї', 'ґ' };

        private static readonly char[] UkarainianReplace = { 'i', 'r' };

        private const string HttpPrefix = "http://";

        #endregion

        #region Constructors and Destructors

        /// <summary>
        ///     Initializes static members of the
        ///     <see cref="StringExtensions" /> class.
        ///     A static constructor is called at most one time, before any instance constructor is invoked or member is accessed.
        /// </summary>
        static StringExtensions()
        {
            DiacriticsMappings = GenerateDiacriticsMappings();
        }

        #endregion

        #region Public Methods and Operators

        public static bool HasDiacritics(this string source)
        {
            return source != source.RemoveDiacritics();
        }

        /// <summary>
        ///     Replaces the diacritics in the given source string.
        ///     See http://en.wikipedia.org/wiki/Diacritic for a complete list currently known of diacritics.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>System.String.</returns>
        public static string RemoveDiacritics(this string source)
        {
            int startIndex = 0;
            int currentIndex = 0;
            var result = new StringBuilder(source.Length);

            while ((currentIndex = source.IndexOfAny(DiacriticsMappings.Keys.ToArray(), startIndex)) != -1)
            {
                result.Append(source.Substring(startIndex, currentIndex - startIndex));
                result.Append(DiacriticsMappings[source[currentIndex]]);

                startIndex = currentIndex + 1;
            }

            if (startIndex == 0)
            {
                return source;
            }

            result.Append(source.Substring(startIndex));

            return result.ToString();
        }

        #endregion

        #region Methods

        private static Dictionary<Char, Char> GenerateDiacriticsMappings()
        {
            var accentsList = new List<Char>();
            accentsList.AddRange(ArabicAccents);
            accentsList.AddRange(BulgarianAccents);
            accentsList.AddRange(CatalanAccents);
            accentsList.AddRange(CroatianAccents);
            accentsList.AddRange(CzechAccents);
            accentsList.AddRange(DutchAccents);
            accentsList.AddRange(EnglishAccents);
            accentsList.AddRange(EstonianAccents);
            accentsList.AddRange(FilipinoAccents);
            accentsList.AddRange(FrenchAccents);
            accentsList.AddRange(GermanAccents);
            accentsList.AddRange(GreekAccents);
            accentsList.AddRange(HungarianAccents);
            accentsList.AddRange(IcelandicAccents);
            accentsList.AddRange(ItalianAccents);
            accentsList.AddRange(LatvianAccents);
            accentsList.AddRange(PolishAccents);
            accentsList.AddRange(PortugueseAccents);
            accentsList.AddRange(RomanianAccents);
            accentsList.AddRange(RussianAccents);
            accentsList.AddRange(SlovakianAccents);
            accentsList.AddRange(SpanishAccents);
            accentsList.AddRange(TurkishAccents);
            accentsList.AddRange(UkarainianAccents);

            var replacementList = new List<Char>();
            replacementList.AddRange(ArabicReplace);
            replacementList.AddRange(BulgarianReplace);
            replacementList.AddRange(CatalanReplace);
            replacementList.AddRange(CroatianReplace);
            replacementList.AddRange(CzechReplace);
            replacementList.AddRange(DutchReplace);
            replacementList.AddRange(EnglishReplace);
            replacementList.AddRange(EstonianReplace);
            replacementList.AddRange(FilipinoReplace);
            replacementList.AddRange(FrenchReplace);
            replacementList.AddRange(GermanReplace);
            replacementList.AddRange(GreekReplace);
            replacementList.AddRange(HungarianReplace);
            replacementList.AddRange(IcelandicReplace);
            replacementList.AddRange(ItalianReplace);
            replacementList.AddRange(LatvianReplace);
            replacementList.AddRange(PolishReplace);
            replacementList.AddRange(PortugueseReplace);
            replacementList.AddRange(RomanianReplace);
            replacementList.AddRange(RussianReplace);
            replacementList.AddRange(SlovakianReplace);
            replacementList.AddRange(SpanishReplace);
            replacementList.AddRange(TurkishReplace);
            replacementList.AddRange(UkarainianReplace);

            Dictionary<char, char> keyValueList = accentsList.Zip(replacementList, (k, v) => new { k, v }).GroupBy(x => x.k).ToDictionary(x => x.Key, x => x.First().v);

            return keyValueList;
            ////return accentsList.Zip(replacementList, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
        }

        #endregion

        /// <summary>
        ///     To the URI.
        /// </summary>
        /// <param name="url">The URL.</param>
        /// <returns>Uri.</returns>
        public static Uri ToUri(this string url)
        {
            if (string.IsNullOrEmpty(url))
            {
                return null;
            }

            if (url.ToLower().StartsWith(HttpPrefix))
            {
                return new Uri(url);
            }

            return new Uri(string.Format("{0}{1}", HttpPrefix, url));
        }

        /// <summary>
        ///     To the unique identifier.
        /// </summary>
        /// <returns>Guid.</returns>
        ////public static Guid ToGuid(this string src)
        ////{
        ////    byte[] stringbytes = Encoding.UTF8.GetBytes(src);
        ////    byte[] hashedBytes = new System.Security.Cryptography.SHA1Managed().ComputeHash(stringbytes);
        ////    Array.Resize(ref hashedBytes, 16);
        ////    return new Guid(hashedBytes);
        ////}
        
        public static bool Like(this string toSearch, string toFind)
        {
            return
                new Regex(@"\A" + new Regex(@"\.|\$|\^|\{|\[|\(|\||\)|\*|\+|\?|\\").Replace(toFind, ch => @"\" + ch).Replace('_', '.').Replace("%", ".*") + @"\z", RegexOptions.Singleline).IsMatch(
                    toSearch);
        }

        /// <summary>Returns a value indicating whether a specified substring <paramref name="value"/> occurs within the source string <paramref name="source"/>.</summary>
        /// <param name="source">The source string.</param>
        /// <param name="value">The string to seek.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        /// <returns>true if the <paramref name="value">value</paramref> parameter occurs within this string, or if <paramref name="value">value</paramref> is the empty string (""); otherwise, false.</returns>
        /// <exception cref="T:System.ArgumentNullException"><paramref name="value">value</paramref> is null.</exception>
        /// <exception cref="T:System.ArgumentException"><paramref name="comparisonType">comparisonType</paramref> is not a valid <see cref="T:System.StringComparison"></see> value.</exception>
        public static bool Contains(this string source, string value, StringComparison comparisonType)
        {
            return source?.IndexOf(value, comparisonType) >= 0;
        }

        /// <summary>
        /// Checks if any of the given strings <paramref name="strings"/> is contained in source string <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="strings">The enumeration of strings to be compared against the source string.</param>
        public static bool ContainsAny(this string source, IEnumerable<string> strings)
        {
            return strings.Any(s => source.Contains(s));
        }

        /// <summary>
        /// Checks if any of the given strings <paramref name="strings"/> is contained in source string <paramref name="source"/>.
        /// </summary>
        /// <param name="source">The source string.</param>
        /// <param name="strings">The enumeration of strings to be compared against the source string.</param>
        /// <param name="comparisonType">One of the enumeration values that specifies the rules for the search.</param>
        public static bool ContainsAny(this string source, IEnumerable<string> strings, StringComparison comparisonType)
        {
            return strings.Any(s => source.Contains(s, comparisonType));
        }

        public static bool StartsWithAny(this string source, IEnumerable<string> strings)
        {
            return strings.Any(s => source.StartsWith(s));
        }

        /// <summary>
        /// Converts the first character of <paramref name="s"/> to upper case.
        /// </summary>
        public static string ToUpperFirst(this string s)
        {
            if (s == null)
            {
                return null;
            }

            if (s == "")
            {
                return "";
            }

            var a = s.ToCharArray();
            a[0] = char.ToUpper(a[0]);
            return new string(a);
        }

        public static readonly char[] TrimNewLineChars = Environment.NewLine.ToCharArray();
        public static readonly char[] TrimChars = $"{Environment.NewLine} ".ToCharArray();

        /// <summary>
        ///     Removes all leading and trailing occurrences of new line (\n\r) as well as white-space characters in an array from
        ///     the current <see cref="T:System.String"></see> object.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <returns>
        ///     The string that remains after all occurrences of trim characters are removed from the start and end of the current
        ///     string.
        /// </returns>
        public static string TrimStartAndEnd(this string str)
        {
            return TrimStartAndEnd(str, TrimChars);
        }

        /// <summary>
        ///     Removes all leading and trailing occurrences of a set of characters specified in an array from the current
        ///     <see cref="T:System.String"></see> object.
        /// </summary>
        /// <param name="str">Input string.</param>
        /// <param name="trimChars">An array of Unicode characters to remove, or null.</param>
        /// <returns>
        ///     The string that remains after all occurrences of characters in the <paramref name="trimChars">trimChars</paramref>
        ///     parameter are removed from the start and end of the current string.
        ///     If <paramref name="trimChars">trimChars</paramref> is null or an empty array, white-space characters are removed
        ///     instead.
        /// </returns>
        public static string TrimStartAndEnd(this string str, params char[] trimChars)
        {
            if (str == null)
            {
                return null;
            }

            return str
                .TrimStart(trimChars)
                .TrimEnd(trimChars);
        }

        public static string RemoveEmptyLines(this string str)
        {
            if (str == null)
            {
                return null;
            }

            var lines = str.Split(TrimNewLineChars, StringSplitOptions.RemoveEmptyEntries);

            var stringBuilder = new StringBuilder(str.Length);

            foreach (var line in lines)
            {
                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString().TrimEnd(TrimNewLineChars);
        }
    }
}