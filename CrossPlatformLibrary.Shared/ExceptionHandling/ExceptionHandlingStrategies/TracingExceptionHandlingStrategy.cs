using System;

using Guards;

using Tracing;

namespace CrossPlatformLibrary.ExceptionHandling.ExceptionHandlingStrategies
{
    /// <summary>
    /// Provides a <see cref="IExceptionHandlingStrategy"/> that traces the exception as FatalError to an <see cref="ITracer"/>.
    /// Exceptions are always rethrown. 
    /// </summary>
    public class TracingExceptionHandlingStrategy : IExceptionHandlingStrategy
    {
        private readonly ITracer tracer;

        public TracingExceptionHandlingStrategy(ITracer tracer)
        {
            Guard.ArgumentNotNull(() => tracer);

            this.tracer = tracer;
        }

        public bool HandleException(Exception exception)
        {
            // Write exception to tracer
            this.tracer.FatalError(exception);

            // Return always false to not handle the exception
            return false;
        }
    }
}