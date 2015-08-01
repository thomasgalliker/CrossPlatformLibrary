using System;
using System.Globalization;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tracing
{
    public class TraceEntry
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TraceEntry"/> class.
        /// </summary>
        /// <param name="category">The category this trace entry belongs to.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="arguments">The arguments used to format the <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="message"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="message"/> parameter is an empty string.</exception>
        /// <exception cref="FormatException">The <paramref name="message"/> is invalid for formatting.
        /// -or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the <paramref name="arguments"/> array.</exception>
        /// <remarks>The <paramref name="message"/> is formatted using the <see cref="CultureInfo.InvariantCulture"/>.</remarks>
        public TraceEntry(Category category, string message, params object[] arguments)
            : this(category, null, message, arguments)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TraceEntry"/> class.
        /// </summary>
        /// <param name="category">The category this trace entry belongs to.</param>
        /// <param name="exception">The exception that produced this trace entry.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="arguments">The arguments used to format the <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="message"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="message"/> parameter is an empty string.</exception>
        /// <exception cref="FormatException">The <paramref name="message"/> is invalid for formatting.
        /// -or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the <paramref name="arguments"/> array.</exception>
        /// <remarks>The <paramref name="message"/> is formatted using the <see cref="CultureInfo.InvariantCulture"/>.</remarks>
        public TraceEntry(Category category, Exception exception, string message, params object[] arguments)
        {
            Guard.ArgumentNotNullOrEmpty(() => message);

            this.Category = category;
            this.Exception = exception;
            if (arguments != null && arguments.Length > 0)
            {
                this.Message = string.Format(CultureInfo.InvariantCulture, message, arguments);
            }
            else
            {
                this.Message = message;
            }
        }

        /// <summary>
        /// Gets the category this trace entry belongs to.
        /// </summary>
        /// <value>The category this trace entry belongs to.</value>
        public Category Category { get; private set; }

        /// <summary>
        /// Gets the exception that produced this trace entry.
        /// </summary>
        /// <value>The exception that produced this trace entry.</value>
        public Exception Exception { get; private set; }

        /// <summary>
        /// Gets the message to trace.
        /// </summary>
        /// <value>The message to trace.</value>
        public string Message { get; private set; }
    }
}