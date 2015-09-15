
using System;
using System.Linq;
using System.Text;

namespace CrossPlatformLibrary.Utils
{
    public static class RandomUtils
    {
        public enum AlphaNumeric
        {
            AlphaNumeric,
            AlphaOnly,
            NumericOnly,
        }

        private const string Alpha = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
        private const string Numeric = "0123456789";

        public static string GenerateRandomString(int length, AlphaNumeric alphaNumeric = AlphaNumeric.AlphaNumeric)
        {
            var sb = new StringBuilder();
            switch (alphaNumeric)
            {
                case AlphaNumeric.AlphaOnly:
                    sb.Append(Alpha);
                    break;

                case AlphaNumeric.NumericOnly:
                    sb.Append(Numeric);
                    break;

                case AlphaNumeric.AlphaNumeric:
                    sb.Append(Alpha);
                    sb.Append(Numeric);
                    break;
            }

            var chars = sb.ToString();
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
