using System.Reflection;
using System.Resources;

namespace CrossPlatformLibrary.Localization
{
    public class ResxSingleTranslationProvider : ITranslationProvider
    {
        private ResourceManager resourceManager;
        private static ResxSingleTranslationProvider instance;

        /// <summary>
        ///     Initializes a new instance of the <see cref="ResxSingleTranslationProvider" /> class.
        /// </summary>
     public static ResxSingleTranslationProvider Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new ResxSingleTranslationProvider();
                }
                return instance;
            }
        }

        public void Init(string baseName, Assembly assembly)
        {
            this.resourceManager = new ResourceManager(baseName, assembly);
        }

        public void Init(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }

        /// <summary>
        ///     See <see cref="ITranslationProvider.Translate" />
        /// </summary>
        public string Translate(string key)
        {
            var translatedValue = this.resourceManager.GetString(key);
            if (translatedValue != null)
            {
                return translatedValue;
            }

            return $"#{key}#";
        }
    }
}