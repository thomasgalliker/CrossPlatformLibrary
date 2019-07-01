using System;

namespace CrossPlatformLibrary.Internals
{
    internal static class Guard
    {
        public static void ArgumentNotNull<T>(T param, string paramName)
        {
            if (param == null)
            {
                throw new ArgumentNullException(paramName, $"{paramName} must not be null");
            }
        }

        public static void ArgumentNotNullOrEmpty(string param, string paramName)
        {
            ArgumentNotNull(param, paramName);

            if (param == string.Empty)
            {
                throw new ArgumentException($"{paramName} must not be empty", paramName);
            }
        }

        public static void ArgumentMaxLength(string param, string paramName, int maxLength)
        {
            ArgumentNotNull(param, paramName);
            ArgumentLength(param, paramName, minLength: 1, maxLength: maxLength);
        }

        public static void ArgumentLength(string param, string paramName, int minLength, int maxLength)
        {
            ArgumentNotNull(param, paramName);

            if (param.Length < minLength)
            {
                throw new ArgumentException($"{paramName} have at least {minLength} characters", paramName);
            }

            if (param.Length > maxLength)
            {
                throw new ArgumentException($"{paramName} must not exceed {maxLength} characters", paramName);
            }
        }

        public static void ArgumentMax35Swift(string param, string paramName)
        {
            ArgumentNotNull(param, paramName);
            ArgumentLength(param, paramName, minLength: 1, maxLength: 35);

            // TODO Check SWIFT-Characterset
        }
    }
}
