using System;
using System.Diagnostics;

namespace CrossPlatformLibrary.Internals
{
    public class DebugTracer : ITracer
    {
        private readonly string name;

        public DebugTracer(string name)
        {
            this.name = name;
        }

        public void Write(Category category, string message)
        {
            Debug.WriteLine(message);
        }

        public void Write(Category category, Exception exception, string message)
        {
            Debug.WriteLine(message);
        }
    }
}