using System;
using System.ComponentModel;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Localization
{
    [Preserve(AllMembers = true)]
    public class TranslationData : INotifyPropertyChanged, IDisposable
    {
        private readonly string key;
        private readonly ILocalizer localizer;
        private readonly ITranslationProvider translationProvider;

        public TranslationData(string key, ILocalizer localizer, ITranslationProvider translationProvider)
        {
            this.key = key;
            this.localizer = localizer;
            this.translationProvider = translationProvider;

            this.localizer.CultureInfoChangedEvent += this.OnLocaleChanged;
        }

        private void OnLocaleChanged(object sender, EventArgs e)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Value)));
        }

        ~TranslationData()
        {
            this.Dispose(false);
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.localizer.CultureInfoChangedEvent -= this.OnLocaleChanged;
            }
        }

        public object Value
        {
            get
            {
                return this.translationProvider.Translate(this.key);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}