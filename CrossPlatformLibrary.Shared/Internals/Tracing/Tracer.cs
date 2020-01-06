using System;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Internals
{
    public static class Tracer
    {
        public static ITracer Create<T>(T target) 
        {
            return new DebugTracer(typeof(T).GetFormattedName());
        }

        public static ITracer Create<T>()
        {
            return new DebugTracer(typeof(T).GetFormattedName());
        }
    }
}