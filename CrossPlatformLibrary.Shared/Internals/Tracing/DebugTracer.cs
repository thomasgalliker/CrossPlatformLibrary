using System;

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
            System.Diagnostics.Debug.WriteLine($"{DateTime.UtcNow}|{this.name}|{category}|{message}[EOL]");
        }

        public void Write(Category category, Exception exception, string message)
        {
            System.Diagnostics.Debug.WriteLine($"{DateTime.UtcNow}|{this.name}|{category}|{message}|{exception}[EOL]");
        }
    }
}