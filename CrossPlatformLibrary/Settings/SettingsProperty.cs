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

        public SettingsProperty(ISettingsService settingsService, Expression<Func<T>> expression, T defaultValue = default(T))
            : this(settingsService, ((MemberExpression)expression.Body).Member.Name, defaultValue)
        {
        }

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
                return this.settingsService.GetValueOrDefault(this.key, this.defaultValue);
            }
            set
            {
                this.settingsService.AddOrUpdateValue(this.key, value);
            }
        }

        object ISettingsProperty.Value
        {
            get
            {
                return this.Value;
            }

            set
            {
                this.Value = (T)value;
            }
        }
    }

    public interface ISettingsProperty
    {
        object Value { get; set; }
    }
}