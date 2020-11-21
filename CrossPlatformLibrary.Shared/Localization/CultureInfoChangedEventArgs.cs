using System;
using System.Globalization;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Localization
{
    [Preserve(AllMembers = true)]
    public class CultureInfoChangedEventArgs : EventArgs
    {
        public CultureInfoChangedEventArgs(CultureInfo cultureInfo)
        {
            this.CultureInfo = cultureInfo;
        }

        public CultureInfo CultureInfo { get; private set; }
    }
}