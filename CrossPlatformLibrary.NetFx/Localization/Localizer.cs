using System;
using System.Globalization;
using System.Threading;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Localization
{
    [Preserve(AllMembers = true)]
    public class Localizer : ILocalizer
    {
        private static readonly Lazy<ILocalizer> Implementation = new Lazy<ILocalizer>(CreateLocalizer, LazyThreadSafetyMode.PublicationOnly);

        public static ILocalizer Current => Implementation.Value;

        private static ILocalizer CreateLocalizer()
        {
            return new Localizer();
        }

        public void SetCultureInfo(CultureInfo cultureInfo)
        {
            if (cultureInfo == null)
            {
                throw new ArgumentNullException(nameof(cultureInfo));
            }

            if (!cultureInfo.Equals(Thread.CurrentThread.CurrentCulture))
            {
                CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
                CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

                Thread.CurrentThread.CurrentCulture = cultureInfo;
                Thread.CurrentThread.CurrentUICulture = cultureInfo;

                this.OnLocaleChanged(cultureInfo);
            }
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