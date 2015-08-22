using System;

namespace CrossPlatformLibrary.Bootstrapping
{
    /// <summary>
    /// The standard exception thrown when a <see cref="IBootstrapper"/> has an error on starting up an application or service.
    /// </summary>
    public class BootstrappingException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrappingException"/> class.
        /// </summary>
        public BootstrappingException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrappingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        public BootstrappingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BootstrappingException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        /// <param name="innerException">The exception that is the cause of the current exception, or <c>null</c> if no inner exception is specified.</param>
        public BootstrappingException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}