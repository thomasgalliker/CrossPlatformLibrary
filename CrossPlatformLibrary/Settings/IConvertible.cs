using System;

namespace CrossPlatformLibrary.Settings
{
    public interface IConvertible
    {
        object Convert(object value, Type sourceType, Type targetType);
    }
}