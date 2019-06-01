﻿using System;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Internals;
using CrossPlatformLibrary.Settings.Converters;
using CrossPlatformLibrary.Settings.Internals;

namespace CrossPlatformLibrary.Settings
{
    public abstract class SettingsServiceBase : ISettingsService
    {
        private readonly object locker = new object();
        private readonly ConverterRegistry converterRegistry;
        private readonly ITracer tracer;

        protected SettingsServiceBase(ITracer tracer)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));

            this.tracer = tracer;
            this.converterRegistry = new ConverterRegistry();
            this.converterRegistry.RegisterConverter<string, bool>(() => new StringToBoolConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, int>(() => new StringToIntegerConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, Uri>(() => new StringToUriConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, Guid>(() => new StringToGuidConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, float>(() => new StringToFloatConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, double>(() => new StringToDoubleConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, decimal>(() => new StringToDecimalConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, TimeSpan>(() => new StringToTimeSpanConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, DateTime>(() => new StringToDateTimeConverter(), reverse: true);
            this.converterRegistry.RegisterConverter<string, DateTimeOffset>(() => new StringToDateTimeOffsetConverter(), reverse: true);
        }

        protected abstract object GetValueOrDefaultFunction<T>(string key, T defaultValue);

        protected abstract void AddOrUpdateValueFunction<T>(string key, T value);

        /// <summary>
        ///     Checks if the given <paramref name="type" /> is a native type and can be stored without conversion.
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        protected virtual bool IsNativeType(Type type)
        {
            return DefaultNativeTypes.Contains(type);
        }

        private static readonly Type[] DefaultNativeTypes =
        {
            typeof(char),
            typeof(string),
        };

        /// <summary>
        ///     Checks if the given <paramref name="type" /> is convertible to string and can be stored as such.
        /// </summary>
        protected virtual bool IsStringConvertible(Type type)
        {
            return DefaultStringSerializableTypes.Contains(type);
        }

        private static readonly Type[] DefaultStringSerializableTypes =
        {
            typeof(byte),
            typeof(byte?),
            typeof(short),
            typeof(short?),
            typeof(ushort),
            typeof(ushort?),
            typeof(int),
            typeof(int?),
            typeof(uint),
            typeof(uint?),
            typeof(long),
            typeof(long?),
            typeof(ulong),
            typeof(ulong?),
            typeof(float),
            typeof(float?),
            typeof(double),
            typeof(double?),
            typeof(decimal),
            typeof(decimal?),
            typeof(bool),
            typeof(bool?),
            typeof(Uri),
            typeof(Guid),
            typeof(Guid?),
            typeof(DateTime),
            typeof(DateTime?),
            typeof(DateTimeOffset),
            typeof(DateTimeOffset?),
        };

        public T GetValueOrDefault<T>(string key, T defaultValue = default(T))
        {
            Guard.ArgumentNotNullOrEmpty(key, nameof(key));

            object value = defaultValue;
            var targetType = typeof(T);

            lock (this.locker)
            {
                this.tracer.Debug($"{nameof(this.GetValueOrDefault)}<{targetType.GetFormattedName()}>(key: \"{key}\")");

                if (this.IsNativeType(targetType))
                {
                    value = this.GetValueOrDefaultFunction(key, defaultValue);
                }
                else if (this.IsStringConvertible(targetType))
                {
                    value = this.GetValueOrDefaultFunction(key, defaultValue);
                }
                else
                {
                    var xmlSerializedObject = (string)this.GetValueOrDefaultFunction<string>(key, null);
                    if (xmlSerializedObject != null)
                    {
                        value = this.DeserializeFromString(targetType, xmlSerializedObject);
                    }
                }
            }

            return (T)this.converterRegistry.TryConvert(value, typeof(string), targetType);
        }


        public void AddOrUpdateValue<T>(string key, T value)
        {
            Guard.ArgumentNotNullOrEmpty(key, nameof(key));

            lock (this.locker)
            {
                var sourceType = typeof(T);

                this.tracer.Debug($"{nameof(this.AddOrUpdateValue)}<{sourceType.GetFormattedName()}>(key: \"{key}\")");

                if (this.IsNativeType(sourceType))
                {
                    this.AddOrUpdateValueFunction(key, value);
                }
                else if (this.IsStringConvertible(sourceType))
                {
                    string serializedValue = null;
                    if (value != null)
                    {
                        serializedValue = (string)this.converterRegistry.TryConvert(value, sourceType, typeof(string));
                    }

                    this.AddOrUpdateValueFunction(key, serializedValue);
                }
                else
                {
                    string xmlSerializedObject = this.SerializeToString(value);
                    this.AddOrUpdateValueFunction(key, xmlSerializedObject);
                }
            }
        }

        public void RegisterConverter<TSource, TTarget>(Func<IConvertible> converterFactory, bool reverse)
        {
            this.converterRegistry.RegisterConverter<TSource, TTarget>(converterFactory, reverse);
        }

        /// <summary>
        /// Custom conversion logic. Override this method to handle string to object deserialization.
        /// </summary>
        protected virtual object DeserializeFromString(Type targetType, string serializedValue)
        {
            throw new SettingsValueConversionException($"{nameof(this.DeserializeFromString)} for targetType={targetType.GetFormattedName()} is currently not supported by the settings service");
        }

        /// <summary>
        /// Custom conversion logic. Override this method to handle object to string serialization.
        /// </summary>
        protected virtual string SerializeToString<T>(T value)
        {
            var sourceType = typeof(T);
            throw new SettingsValueConversionException($"{nameof(this.SerializeToString)} for targetType={sourceType.GetFormattedName()}  is currently not supported by the settings service");
        }
    }
}