using System;

namespace CrossPlatformLibrary.ExceptionHandling.ExceptionHandlingStrategies
{
    /// <summary>
    /// Provides an <see cref="IExceptionHandlingStrategy"/> that always returns <c>false</c>.
    /// </summary>
    public class RethrowExceptionHandlingStrategy : IExceptionHandlingStrategy
    {
        /// <summary>
        /// Handles the specified <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The exception to handle.</param>
        /// <exception cref="Exception">The specified <paramref name="exception"/> is always thrown;
        /// expect the <paramref name="exception"/> parameter is <c>null</c>.</exception>
        /// <returns><c>True</c> if the specified <paramref name="exception"/> was successfully handled; otherwise, <c>false</c>.</returns>
        /// <remarks>The <see cref="RethrowExceptionHandlingStrategy"/> always returns <c>false</c> meaning the <paramref name="exception"/> was
        /// not handled and should be rethrown.</remarks>
        public bool HandleException(Exception exception)
        {
            return false;
        }
    }
}