using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CrossPlatformLibrary.Tracing
{
    public static class TracerExtensions
    {
        [DebuggerStepThrough]
        public static void Info(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Information, message, arguments);
        }

        [DebuggerStepThrough]
        public static void Debug(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Debug, message, arguments);
        }

        [DebuggerStepThrough]
        public static void Warning(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Warning, message, arguments);
        }

        [DebuggerStepThrough]
        public static void Error(this ITracer tracer, string message, params object[] arguments)
        {
            tracer.Write(Category.Error, message, arguments);
        }


        [DebuggerStepThrough]
        public static void Exception(this ITracer tracer, Exception exception, string message = null, params object[] arguments)
        {
            tracer.Write(Category.Error, exception, message, arguments);
        }

        [DebuggerStepThrough]
        public static void Exception(this ITracer tracer, Exception exception, string message = null, [CallerMemberName]string callerMemberName = "")
        {
            exception.Data["callerMemberName"] = callerMemberName;
            tracer.Write(Category.Error, exception, message);
        }

        /// <summary>
        /// This call is used when the application is about to crash.
        /// There is usually only limited time and resources to report a fatal error.
        /// </summary>
        [DebuggerStepThrough]
        public static void FatalError(this ITracer tracer, Exception exception)
        {
            tracer.Write(Category.Fatal, exception, "FatalError");
        }
    }
}