using System;
using System.Globalization;
using System.Reflection;

using CrossPlatformLibrary.Utils;

namespace CrossPlatformLibrary.Tracing
{
    public abstract class TracerFactoryBase : ITracerFactory
    {
        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the specified <paramref name="name"/>.
        /// </summary>
        /// <param name="name">The name of the tracer.</param>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="name"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="name"/> parameter is an empty string.</exception>
        public abstract ITracer Create(string name);

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the <see cref="Type.FullName"/> of the specified <paramref name="type"/>.
        /// </summary>
        /// <param name="type">The type whose fully qualified type name is used as the name of the tracer.</param>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> parameter is <c>null</c>.</exception>
        public virtual ITracer Create(Type type)
        {
            Guard.ArgumentNotNull(() => type);

            string name = GetTypeNameAndAssemblyVersion(type);
            return this.Create(name);
        }

        /// <summary>
        /// Creates a <see cref="ITracer"/> whose name is set to the <see cref="Type.FullName"/> of the specified <typeparamref name="T"/>.
        /// </summary>
        /// <typeparam name="T">The type whose fully qualified type name is used as the name of the tracer.</typeparam>
        /// <returns>A new <see cref="ITracer"/> instance.</returns>
        public virtual ITracer Create<T>()
        {
            return this.Create(typeof(T));
        }

        private static string GetTypeNameAndAssemblyVersion(Type type)
        {
            var fileVersion = string.Empty;

            var assembly = type.GetTypeInfo().Assembly;

            var assemblyFileVersionAttribute = assembly.GetCustomAttribute(typeof(AssemblyFileVersionAttribute)) as AssemblyFileVersionAttribute;
            if (assemblyFileVersionAttribute != null)
            {
                fileVersion = assemblyFileVersionAttribute.Version;
            }

            if (string.IsNullOrEmpty(fileVersion))
            {
                return type.FullName;
            }
            return string.Format(CultureInfo.InvariantCulture, "{0}[{1}]", type.FullName, fileVersion);
        }
    }
}