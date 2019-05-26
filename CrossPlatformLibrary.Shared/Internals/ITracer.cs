using System;

namespace CrossPlatformLibrary.Internals
{
    public interface ITracer
    {
        void Write(Category category, string message);

        void Write(Category category, Exception exception, string message);
    }
}
