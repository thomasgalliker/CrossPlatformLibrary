using System;

using CrossPlatformLibrary.Tracing;
using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.ExceptionHandling.Handlers
{
    /// <summary>
    /// Provides a <see cref="IExceptionHandler"/> that traces the exception as FatalError to an <see cref="ITracer"/>.
    /// Exceptions are always rethrown. 
    /// </summary>
    public class TracingExceptionHandler : ExceptionHandlerBase
    {
        private readonly ITracer tracer;

        public TracingExceptionHandler(ITracer tracer, IPlatformSpecificExceptionHandler platformSpecificExceptionHandler)
            : base(platformSpecificExceptionHandler)
        {
            Guard.ArgumentNotNull(() => tracer);

            this.tracer = tracer;
        }

        public override bool HandleException(Exception exception)
        {
            // Write exception to tracer
            this.tracer.FatalError(exception);

            // Return always false to not handle the exception
            return false;
        }
    }
}