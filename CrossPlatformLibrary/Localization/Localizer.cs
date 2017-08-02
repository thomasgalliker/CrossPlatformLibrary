using System;
using System.Globalization;

namespace CrossPlatformLibrary.Localization
{
    public class Localizer : ILocalizer
    {
        public CultureInfo GetCurrentCulture()
        {
            throw new NotImplementedInReferenceAssemblyException();
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            throw new NotImplementedInReferenceAssemblyException();
        }

        public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;
    }
}
