using System;
using Newtonsoft.Json;

namespace CrossPlatformLibrary.Tests.Settings
{
    public class AppSettingsJsonConverter : CrossPlatformLibrary.Settings.IConvertible
    {
        public object Convert(object value, Type sourceType, Type targetType)
        {
            if (sourceType == typeof(string))
            {
                return JsonConvert.DeserializeObject(value as string ?? "", targetType);
            }
            else if (targetType == typeof(string))
            {
                return JsonConvert.SerializeObject(value);
            }
            else
            {
                throw new NotSupportedException($"AppSettingsJsonConverter: Conversion from {sourceType.Name} to {targetType.Name} is not supported");
            }
        }
    }
}
