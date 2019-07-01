using System;
using System.Linq.Expressions;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.Settings
{
    public class SettingsProperty<T> : ISettingsProperty
    {
        private readonly ISettingsService settingsService;
        private readonly string key;
        private readonly T defaultValue;
        private CachedValue<T> cachedValue;

        public SettingsProperty(ISettingsService settingsService, Expression<Func<T>> expression, T defaultValue = default(T))
            : this(settingsService, ((MemberExpression)expression.Body).Member.Name, defaultValue)
        {
        }

        /// <summary>
        ///     Turns property value caching on/off.
        ///     Default: <c>True</c> = on.
        /// </summary>
        public bool CachingEnabled { get; set; } = true;

        public SettingsProperty(ISettingsService settingsService, string key, T defaultValue = default(T))
        {
            Guard.ArgumentNotNull(settingsService, nameof(settingsService));
            Guard.ArgumentNotNullOrEmpty(key, nameof(key));

            if (key.Length > 255)
            {
                throw new ArgumentException($"{nameof(key)} must not exceed length of 255 characters", nameof(key));
            }

            this.settingsService = settingsService;
            this.key = key;
            this.defaultValue = defaultValue;
        }

        public T Value
        {
            get
            {
                if (this.CachingEnabled && this.cachedValue.HasValue)
                {
                    return this.cachedValue.Value;
                }

                var value = this.settingsService.GetValueOrDefault(this.key, this.defaultValue);

                if (this.CachingEnabled)
                {
                    this.cachedValue.Value = value;
                }

                return value;
            }
            set
            {
                if (this.CachingEnabled)
                {
                    this.cachedValue.Value = value;
                }

                this.settingsService.AddOrUpdateValue(this.key, value);
            }
        }

        object ISettingsProperty.Value
        {
            get => this.Value;

            set => this.Value = (T)value;
        }
    }

    internal struct CachedValue<T>
    {
        private T value;

        public bool HasValue { get; private set; }

        public T Value
        {
            get => this.value;
            set
            {
                this.value = value;
                this.HasValue = true;
            }
        }
    }

    public interface ISettingsProperty
    {
        object Value { get; set; }
    }
}