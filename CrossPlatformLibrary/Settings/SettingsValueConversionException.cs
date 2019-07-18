using System;

namespace CrossPlatformLibrary.Settings
{
    public class SettingsValueConversionException : Exception
    {
        public SettingsValueConversionException(string message) : base(message)
        {
        }
    }
}