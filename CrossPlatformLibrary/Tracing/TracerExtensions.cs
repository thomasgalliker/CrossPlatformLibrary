using System;
using System.Runtime.CompilerServices;

namespace CrossPlatformLibrary.Tracing
{
    public static class TracerExtensions
    {
        public static void Info(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Information, message, arguments);
        }

        public static void Debug(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Debug, message, arguments);
        }

        public static void Warning(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Warning, message, arguments);
        }

        public static void Error(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Error, message, arguments);
        }

        public static void Exception(this ITracer tracer, Exception exception, string message = null, [CallerMemberName]string callerMemberName = "")
        {
            exception.Data["callerMemberName"] = callerMemberName;
            tracer.Write(Category.Error, exception, message);
        }

        /// <summary>
        /// This call is used when the application is about to crash.
        /// There is usually only limited time and resources to report a fatal error.
        /// </summary>
        public static void FatalError(this ITracer tracer, Exception exception)
        {
            tracer.Write(Category.Fatal, exception, "FatalError");
        }
    }
}