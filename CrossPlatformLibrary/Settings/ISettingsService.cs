
using System;

namespace CrossPlatformLibrary.Settings
{
    public interface ISettingsService
    {
        /// <summary>
        ///     Gets the value for a specified key or the default (if key not found).
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key for settings</param>
        /// <param name="defaultValue">default value if not set</param>
        /// <returns>Value or default</returns>
        T GetValueOrDefault<T>(string key, T defaultValue = default(T));

        /// <summary>
        /// Creates or updates the value for the given key.
        /// </summary>
        /// <typeparam name="T">Type of value.</typeparam>
        /// <param name="key">Key for settings</param>
        /// <param name="value">Value for settings</param>
        void AddOrUpdateValue<T>(string key, T value);

        void RegisterConverter<TSource, TTarget>(Func<IConvertible> converterFactory, bool reverse);

        ////void Remove(string key);

        ////void RemoveAll();
    }
}