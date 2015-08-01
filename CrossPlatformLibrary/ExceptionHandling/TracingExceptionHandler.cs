using System;

using CrossPlatformLibrary.Tracing;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    /// Provides a <see cref="IExceptionHandler"/> that traces the exception to an <see cref="ITracer"/>.
    /// Exceptions are always rethrown. 
    /// </summary>
    public class TracingExceptionHandler : ExceptionHandlerBase
    {
        private readonly ITracer tracer;

        public TracingExceptionHandler()
        {
            this.tracer = Tracer.Create(this);
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