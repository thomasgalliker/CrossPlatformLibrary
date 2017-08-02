using System;
using System.Globalization;
using System.Threading;

namespace CrossPlatformLibrary.Localization
{
    public class Localizer : ILocalizer
    {
        static readonly Lazy<ILocalizer> Implementation = new Lazy<ILocalizer>(CreateResourceLoader, LazyThreadSafetyMode.PublicationOnly);

        public static ILocalizer Current
        {
            get
            {
                return Implementation.Value;
            }
        }

        static ILocalizer CreateResourceLoader()
        {
            return new Localizer();
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
#if WINDOWS_UWP
            CultureInfo.CurrentCulture = cultureInfo;
            CultureInfo.CurrentUICulture = cultureInfo;
#endif

            this.OnLocaleChanged(cultureInfo);
        }

        public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;

        protected virtual void OnLocaleChanged(CultureInfo ci)
        {
            this.CultureInfoChangedEvent?.Invoke(this, new CultureInfoChangedEventArgs(ci));
        }

        public CultureInfo GetCurrentCulture()
        {
            return CultureInfo.CurrentCulture;
        }
    }
}