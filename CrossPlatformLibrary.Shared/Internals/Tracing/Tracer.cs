using System;
using CrossPlatformLibrary.Extensions;

namespace CrossPlatformLibrary.Internals
{
    public static class Tracer
    {
        private static readonly Lazy<ITracer> defaultLogger = new Lazy<ITracer>(CreateDefaultLogger, System.Threading.LazyThreadSafetyMode.PublicationOnly);
        private static ITracer logger;

        private static ITracer CreateDefaultLogger()
        {
            return new DebugTracer("CrossPlatformLibrary");
        }

        public static void SetLogger(ITracer logger)
        {
            Tracer.logger = logger;
        }

        public static ITracer Current => Tracer.logger ?? defaultLogger.Value;

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