using System;
using System.Collections.Generic;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Settings.Internals
{
    internal class ConverterRegistry
    {
        private readonly Dictionary<Tuple<Type, Type>, IConvertible> converters;

        public ConverterRegistry()
        {
            this.converters = new Dictionary<Tuple<Type, Type>, IConvertible>();
        }

        internal void RegisterConverter<TSource, TTarget>(IConvertible converterFactory, bool reverse)
        {
            lock (this.converters)
            {
                this.converters.Add(new Tuple<Type, Type>(typeof(TSource), typeof(TTarget)), converterFactory);
                if (reverse)
                {
                    this.converters.Add(new Tuple<Type, Type>(typeof(TTarget), typeof(TSource)), converterFactory);
                }
            }
        }

        public object TryConvert(object value, Type sourceType, Type targetType)
        {
            if (sourceType == targetType || (value != null && value.GetType() == targetType))
            {
                // No need for conversion if source and target types are the same
                return value;
            }

            var factory = this.GetConverterForType(sourceType, targetType);
            var converted = factory.Convert(value, sourceType, targetType);
            return converted;
        }

        private IConvertible GetConverterForType(Type sourceType, Type targetType)
        {
            lock (this.converters)
            {
                var key = new Tuple<Type, Type>(sourceType, targetType);
                if (this.converters.ContainsKey(key))
                {
                    var converterFactory = this.converters[key];
                    return converterFactory;
                }

                throw new SettingsValueConversionException($"Missing converter for sourceType={sourceType.GetFormattedName()} and targetType={targetType.GetFormattedName()}");
            }
        }
    }
}