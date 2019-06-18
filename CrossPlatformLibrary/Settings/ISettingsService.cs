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

        /// <summary>
        /// Registers a custom conversion logic to convert from type <typeparamref name="TSource"/> to <typeparamref name="TTarget"/>.
        /// </summary>
        /// <typeparam name="TSource">The source type.</typeparam>
        /// <typeparam name="TTarget">The target type.</typeparam>
        /// <param name="convertible">The instance of IConvertible.</param>
        /// <param name="reverse">The flag <paramref name="reverse"/> indicates if the converter works in both directions.</param>
        void RegisterConverter<TSource, TTarget>(IConvertible convertible, bool reverse);

        /// <summary>
        /// The default conversion logic which kicks in if no other matching <code>IConvertible</code> is registered.
        /// </summary>
        /// <param name="convertible">The instance of IConvertible.</param>
        void RegisterDefaultConverter(IConvertible convertible);

        ////void Remove(string key);

        ////void RemoveAll();
    }
}