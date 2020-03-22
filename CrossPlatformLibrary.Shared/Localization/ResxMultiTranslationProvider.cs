using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using System.Threading;

namespace CrossPlatformLibrary.Localization
{
    public class ResxMultiTranslationProvider : ITranslationProvider
    {
        static readonly Lazy<ResxMultiTranslationProvider> Implementation = new Lazy<ResxMultiTranslationProvider>(CreateTranslationProvider, LazyThreadSafetyMode.PublicationOnly);

        public static ResxMultiTranslationProvider Current => Implementation.Value;

        private static ResxMultiTranslationProvider CreateTranslationProvider()
        {
            return new ResxMultiTranslationProvider();
        }

        private readonly List<ResourceManager> resourceManagers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResxMultiTranslationProvider" /> class.
        /// </summary>
        private ResxMultiTranslationProvider()
        {
            this.resourceManagers = new List<ResourceManager>();
        }

        /// <summary>
        ///     Adds a <paramref name="resourceManager" /> which participates in the translation process.
        /// </summary>
        /// <param name="resourceManager">The resource manager used to lookup translations keys.</param>
        public void AddResourceManager(ResourceManager resourceManager)
        {
            this.resourceManagers.Add(resourceManager);
        }

        /// <summary>
        ///     See <see cref="ITranslationProvider.Translate(string key, CultureInfo cultureInfo)" />
        /// </summary>
        public string Translate(string key, CultureInfo cultureInfo = null)
        {
            if (!this.resourceManagers.Any())
            {
                throw new InvalidOperationException($"ResxMultiTranslationProvider must be configured with at least one ResourceManager." +
                                                    $"Call method {nameof(this.AddResourceManager)} before translation is possible.");
            }

            foreach (var resourceManager in this.resourceManagers)
            {
                var translatedValue = resourceManager.GetString(key, cultureInfo);
                if (translatedValue != null)
                {
                    return translatedValue;
                }
            }

            // If none of the configured resource managers contains the key,
            // we return the hashtagged key.
            return $"#{key}#";
        }
    }
}