using System;

namespace CrossPlatformLibrary.Tracing
{
    public interface ITracerFactory
    {
        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the tracer.</param>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="name"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string.</exception>
        ITracer Create(string name);

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the <see cref="Type.FullName"/> of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type whose fully qualified type name is used as the name of the tracer.</param>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> parameter is <c>null</c>.</exception>
        ITracer Create(Type type);

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the <see cref="Type.FullName"/> of the specified <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose fully qualified type name is used as the name of the tracer.</typeparam>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        ITracer Create<T>();
    }
}