using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

using Guards;

namespace CrossPlatformLibrary.Tracing
{
    [DebuggerStepThrough]
    public static class TracerExtensions
    {
        public static void Info(this ITracer tracer, string message, params object[] arguments)
        {
            Guard.ArgumentNotNull(() => tracer);

            tracer.Write(Category.Information, message, arguments);
        }

        public static void Debug(this ITracer tracer, string message, params object[] arguments)
        {
            Guard.ArgumentNotNull(() => tracer);

            tracer.Write(Category.Debug, message, arguments);
        }

        public static void Warning(this ITracer tracer, string message, params object[] arguments)
        {
            Guard.ArgumentNotNull(() => tracer);

            tracer.Write(Category.Warning, message, arguments);
        }

        public static void Error(this ITracer tracer, string message, params object[] arguments)
        {
            Guard.ArgumentNotNull(() => tracer);

            tracer.Write(Category.Error, message, arguments);
        }

        public static void Exception(this ITracer tracer, Exception exception, string message = null, params object[] arguments)
        {
            Guard.ArgumentNotNull(() => tracer);
            Guard.ArgumentNotNull(() => exception);

            if (message == null)
            {
                message = exception.Message;
            }

            tracer.Write(Category.Error, exception, message, arguments);
        }

        public static void Exception(this ITracer tracer, Exception exception, string message = null, [CallerMemberName]string callerMemberName = "")
        {
            exception.Data["callerMemberName"] = callerMemberName;
            tracer.Exception(exception, message, new object[]{});
        }

        /// <summary>
        /// This call is used when the application is about to crash.
        /// There is usually only limited time and resources to report a fatal error.
        /// </summary>
        public static void FatalError(this ITracer tracer, Exception exception)
        {
            Guard.ArgumentNotNull(() => tracer);

            tracer.Write(Category.Fatal, exception, "FatalError");
        }
    }
}