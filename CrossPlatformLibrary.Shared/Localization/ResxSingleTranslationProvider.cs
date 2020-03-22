using System;
using System.Globalization;
using System.Reflection;
using System.Resources;
using System.Threading;

namespace CrossPlatformLibrary.Localization
{
    public class ResxSingleTranslationProvider : ITranslationProvider
    {
        static readonly Lazy<ResxSingleTranslationProvider> Implementation = new Lazy<ResxSingleTranslationProvider>(CreateTranslationProvider, LazyThreadSafetyMode.PublicationOnly);

        public static ResxSingleTranslationProvider Current => Implementation.Value;

        private static ResxSingleTranslationProvider CreateTranslationProvider()
        {
            return new ResxSingleTranslationProvider();
        }

        private ResourceManager resourceManager;

        public void Init(string baseName, Assembly assembly)
        {
            if (this.resourceManager != null)
            {
                throw new InvalidOperationException($"ResxSingleTranslationProvider can only be initialized once.");
            }

            this.resourceManager = new ResourceManager(baseName, assembly);
        }

        public void Init(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        /// <summary>
        ///     See <see cref="ITranslationProvider.Translate(string, CultureInfo)" />
        /// </summary>
        public string Translate(string key, CultureInfo cultureInfo = null)
        {
            var translatedValue = this.resourceManager.GetString(key, cultureInfo);
            if (translatedValue != null)
            {
                return translatedValue;
            }

            return $"#{key}#";
        }
    }
}