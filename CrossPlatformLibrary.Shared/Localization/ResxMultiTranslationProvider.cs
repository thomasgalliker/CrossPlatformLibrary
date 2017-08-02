using System.Collections.Generic;
using System.Resources;

namespace CrossPlatformLibrary.Localization
{
    public class ResxMultiTranslationProvider : ITranslationProvider
    {
        private readonly List<ResourceManager> resourceManagers;
        private static ResxMultiTranslationProvider instance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResxMultiTranslationProvider" /> class.
        /// </summary>
        public ResxMultiTranslationProvider()
        {
            this.resourceManagers = new List<ResourceManager>();
        }

        public static ITranslationProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResxMultiTranslationProvider();
                }
                return instance;
            }
        }

        public void AddResourceManager(ResourceManager resourceManager)
        {
            this.resourceManagers.Add(resourceManager);
        }

        /// <summary>
        ///     See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public string Translate(string key)
        {
            foreach (var resourceManager in this.resourceManagers)
            {
                var translatedValue = resourceManager.GetString(key);
                if (translatedValue != null)
                {
                    return translatedValue;
                }

////                var translation = resourceManager.GetString(key);
////                if (translation == null)
////                {
////#if DEBUG
////                    throw new ArgumentException($"Key '{key}' was not found for culture cultureInfo.Name.", nameof(key));
////#else
////				    translation = resourceKey; // HACK: returns the key, which GETS DISPLAYED TO THE USER
////#endif
////                }

////                return translation;
            }

            return $"#{key}#";
        }
    }
}