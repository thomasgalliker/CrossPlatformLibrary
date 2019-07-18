using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace CrossPlatformLibrary.Internals
{
    [DebuggerStepThrough]
    public static class TracerExtensions
    {
        public static void Info(this ITracer tracer, string message)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));

            tracer.Write(Category.Information, message);
        }

        public static void Debug(this ITracer tracer, string message)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));

            tracer.Write(Category.Debug, message);
        }

        public static void Warning(this ITracer tracer, string message)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));

            tracer.Write(Category.Warning, message);
        }

        public static void Error(this ITracer tracer, string message)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));

            tracer.Write(Category.Error, message);
        }

        public static void Exception(this ITracer tracer, Exception exception, string message = null)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));
            Guard.ArgumentNotNull(exception, nameof(exception));

            if (message == null)
            {
                message = exception.Message;
            }

            tracer.Write(Category.Error, exception, message);
        }

        public static void Exception(this ITracer tracer, Exception exception, string message = null, [CallerMemberName]string callerMemberName = "")
        {
            exception.Data["callerMemberName"] = callerMemberName;
            tracer.Exception(exception, message);
        }

        /// <summary>
        /// This call is used when the application is about to crash.
        /// There is usually only limited time and resources to report a fatal error.
        /// </summary>
        public static void FatalError(this ITracer tracer, Exception exception)
        {
            Guard.ArgumentNotNull(tracer, nameof(tracer));
            Guard.ArgumentNotNull(exception, nameof(exception));

            tracer.Write(Category.Fatal, exception, "FatalError");
        }
    }
}