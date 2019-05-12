using System;
using System.Globalization;
using System.Threading;

namespace CrossPlatformLibrary.Localization
{
    public class Localizer : ILocalizer
    {
        static readonly Lazy<ILocalizer> Implementation = new Lazy<ILocalizer>(CreateResourceLoader, LazyThreadSafetyMode.PublicationOnly);

        public static ILocalizer Current => Implementation.Value;

        static ILocalizer CreateResourceLoader()
        {
            return new Localizer();
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            Thread.CurrentThread.CurrentCulture = cultureInfo;
            Thread.CurrentThread.CurrentUICulture = cultureInfo;

            this.OnLocaleChanged(cultureInfo);
        }

        public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;

        protected virtual void OnLocaleChanged(CultureInfo ci)
        {
            this.CultureInfoChangedEvent?.Invoke(this, new CultureInfoChangedEventArgs(ci));
        }

        public CultureInfo GetCurrentCulture()
        {
            return Thread.CurrentThread.CurrentCulture;
        }
    }
}