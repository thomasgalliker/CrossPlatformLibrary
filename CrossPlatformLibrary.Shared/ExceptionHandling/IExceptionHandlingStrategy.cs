using System;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    /// Provides an interface to handle any <see cref="Exception"/> that is not handled by the application.
    /// </summary>
    public interface IExceptionHandlingStrategy
    {
        /// <summary>
        /// Handles the specified <paramref name="exception"/>.
        /// </summary>
        /// <param name="exception">The exception to handle.</param>
        /// <returns><c>True</c> if the specified <paramref name="exception"/> was successfully handled; otherwise, <c>false</c>.</returns>
        bool HandleException(Exception exception);
    }
}