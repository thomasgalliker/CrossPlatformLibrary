using System;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes.Extensions
{
    public static class ResourceDictionaryExtensions
    {
        /// <summary>
        ///     Gets a resource of the specified type from the current <see cref="Application.Resources" />.
        /// </summary>
        /// <typeparam name="T">The type of the resource object to be retrieved.</typeparam>
        /// <param name="resourceDictionary">The resource dictionary.</param>
        /// <param name="key">
        ///     The key of the resource object. For a list of CrossPlatformLibrary resource keys, see
        ///     <see cref="ThemeConstants" />.
        /// </param>
        /// <exception cref="InvalidCastException" />
        /// <exception cref="ArgumentNullException" />
        public static T ResolveTheme<T>(this ResourceDictionary resourceDictionary, string key)
        {
            if (key == null)
            {
                throw new ArgumentNullException(nameof(key));
            }

            var success = resourceDictionary.TryGetValue(key, out var value);
            if (success)
            {
                if (value is T resource)
                {
                    return resource;
                }

                if (value != null)
                {
                    throw new InvalidCastException($"Resource with key='{key}' was not of the type {typeof(T).GetFormattedName()}.");
                }
            }

            throw new InvalidOperationException($"{typeof(T).GetFormattedName()} with key='{key}' could not be found.");
        }

        public static void TryAddColorResource(this ResourceDictionary resourceDictionary, string key, Color color)
        {
            if (key == null || color.IsDefault)
            {
                return;
            }

            resourceDictionary.Add(key, color);
        }
    }
}