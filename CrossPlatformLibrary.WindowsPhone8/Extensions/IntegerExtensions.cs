﻿namespace CrossPlatformLibrary.Extensions
{
    public static class IntegerExtensions
    {
        public static bool IsOdd(this int value)
        {
            return value % 2 != 0;
        }
    }
}