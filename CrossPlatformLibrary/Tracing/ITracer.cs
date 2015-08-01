using System;

using CrossPlatformLibrary.IoC;

namespace CrossPlatformLibrary.Tracing
{
    [UseCache(false)]
    public interface ITracer
    {
        void Write(Category category, string message, params object[] arguments);

        void Write(Category category, Exception exception, string message, params object[] arguments);
    }
}