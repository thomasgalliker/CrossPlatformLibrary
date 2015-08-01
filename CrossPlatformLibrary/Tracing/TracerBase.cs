using System;
using System.Globalization;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tracing
{
    public abstract class TracerBase : ITracer
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="TracerBase"/> class.
        /// </summary>
        protected TracerBase()
        {
        }

        /// <summary>
        /// Traces a new trace entry with the specified message.
        /// </summary>
        /// <param name="category">The category this trace entry belongs to.</param>
        /// <param name="message">The message to trace.</param>
        /// <param name="arguments">The arguments used to format the <paramref name="message"/>.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="message"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="message"/> parameter is an empty string.</exception>
        /// <exception cref="FormatException">The <paramref name="message"/> is invalid for formatting.
        /// -or- The number indicating an argument to format is less than zero, or greater than or equal to the length of the <paramref name="arguments"/> array.</exception>
        /// <remarks>The <paramref name="message"/> is formatted using the <see cref="CultureInfo.InvariantCulture"/>.</remarks>
        public void Write(Category category, string message, params object[] arguments)
        {
            Guard.ArgumentNotNullOrEmpty(() => message);

            if (this.IsCategoryEnabled(category))
            {
                TraceEntry entry = new TraceEntry(category, message, arguments);
                this.WriteCore(entry);
            }
        }

        /// <summary>
        /// Traces a new trace entry with the specified message.
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
        public void Write(Category category, Exception exception, string message, params object[] arguments)
        {
            if (this.IsCategoryEnabled(category))
            {
                TraceEntry entry = new TraceEntry(category, exception, message, arguments);
                this.WriteCore(entry);
            }
        }

        /// <summary>
        /// Traces the specified <paramref name="entry"/>.
        /// </summary>
        /// <param name="entry">The trace entry to trace.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entry"/> parameter is <c>null</c>.</exception>
        public void Write(TraceEntry entry)
        {
            Guard.ArgumentNotNull(() => entry);

            if (this.IsCategoryEnabled(entry.Category))
            {
                this.WriteCore(entry);
            }
        }

        /// <summary>
        /// Traces the specified <paramref name="entry"/>. This is the core method which is called from all public Write methods.
        /// The Check if <paramref name="entry"/> is not null and if <see cref="IsCategoryEnabled"/> is already done.
        /// </summary>
        /// <param name="entry">The trace entry to trace.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="entry"/> parameter is <c>null</c>.</exception>
        protected abstract void WriteCore(TraceEntry entry);

        /// <summary>
        /// Determines whether the specified <paramref name="category"/> is enabled.
        /// </summary>
        /// <param name="category">The category to check if it is enabled.</param>
        /// <returns><c>True</c> if the specified <paramref name="category"/> is enabled; otherwise, <c>false</c>.</returns>
        public abstract bool IsCategoryEnabled(Category category);
    }
}