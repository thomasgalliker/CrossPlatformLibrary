using System;
using System.Threading;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tracing
{
    /// <summary>
    /// Provides tracing functionality and encapsulates the concrete tracing implementation.
    /// </summary>
    public static class Tracer
    {
        private static ITracerFactory internalFactory;

        /// <summary>
        /// Gets the currently active <see cref="ITracerFactory"/>.
        /// </summary>
        /// <value>The currently active <see cref="ITracerFactory"/>.</value>
        public static ITracerFactory Factory
        {
            get
            {
                return internalFactory ?? Tracer.CreateDefaultFactory();
            }
        }

        /// <summary>
        /// Sets the concrete <see cref="ITracerFactory"/> to use within the <see cref="Tracer"/>. 
        /// </summary>
        /// <param name="factory">The <see cref="ITracerFactory"/> to use within the <see cref="Tracer"/>.</param>
        public static void SetFactory(ITracerFactory factory)
        {
            Interlocked.Exchange(ref internalFactory, factory);
        }

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the tracer.</param>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="name"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string.</exception>
        public static ITracer Create(string name)
        {
            Guard.ArgumentNotNullOrEmpty(() => name);

            return internalFactory.Create(name);
        }

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the <see cref="Type.FullName"/> of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type whose fully qualified type name is used as the name of the tracer.</param>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> parameter is <c>null</c>.</exception>
        public static ITracer Create(Type type)
        {
            Guard.ArgumentNotNull(() => type);

            return internalFactory.Create(type);
        }

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the <see cref="Type.FullName"/> of the specified <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose fully qualified type name is used as the name of the tracer.</typeparam>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        public static ITracer Create<T>(T ignoredObject = default(T))
        {
            return internalFactory.Create<T>();
        }

        private static ITracerFactory CreateDefaultFactory()
        {
            return new EmptyTracerFactory();
        }
    }
}