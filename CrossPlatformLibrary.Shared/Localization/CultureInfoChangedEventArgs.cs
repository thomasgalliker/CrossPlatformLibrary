using System;
using System.Globalization;

namespace CrossPlatformLibrary.Localization
{
    public class CultureInfoChangedEventArgs : EventArgs
    {
        public CultureInfoChangedEventArgs(CultureInfo cultureInfo)
        {
            this.CultureInfo = cultureInfo;
        }

        public CultureInfo CultureInfo { get; private set; }
    }
}