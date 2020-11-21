using System;
using System.Globalization;
using CrossPlatformLibrary.Internals;
using CrossPlatformLibrary.Localization;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Localization
{
    [Preserve(AllMembers = true)]
    [ContentProperty("Text")]
    public class TranslateExtension : BindableObject, IMarkupExtension<BindingBase>
    {
        private static ILocalizer localizer = new NullLocalizer();
        private static ITranslationProvider translationProvider = new NullTranslationProvider();

        public static void Init(ILocalizer localizer, ITranslationProvider translationProvider)
        {
            TranslateExtension.localizer = localizer;
            TranslateExtension.translationProvider = translationProvider;
        }

        public string Text { get; set; }

        public BindingBase ProvideValue(IServiceProvider serviceProvider)
        {
            var binding = new Binding(nameof(TranslationData.Value))
            {
                Source = new TranslationData(this.Text, localizer, translationProvider)
            };
            return binding;
        }

        object IMarkupExtension.ProvideValue(IServiceProvider serviceProvider)
        {
            return this.ProvideValue(serviceProvider);
        }

        private class NullTranslationProvider : ITranslationProvider
        {
            public string Translate(string key, CultureInfo cultureInfo)
            {
                return $"Call {nameof(TranslateExtension)}.{nameof(Init)}!";
            }
        }

        private class NullLocalizer : ILocalizer
        {
            public CultureInfo GetCurrentCulture()
            {
                return CultureInfo.CurrentCulture;
            }

            public void SetCultureInfo(CultureInfo cultureInfo)
            {
                CultureInfo.CurrentCulture = cultureInfo;
            }

            public event EventHandler<CultureInfoChangedEventArgs> CultureInfoChangedEvent;
        }
    }
}