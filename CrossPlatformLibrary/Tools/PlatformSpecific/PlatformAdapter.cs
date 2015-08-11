using System;

using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tracing;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    /// <summary>
    ///     Enables types within PclContrib to use platform-specific features in a platform-agnostic way
    /// </summary>
    // TODO GATH: Extract to another PCL-nuget project and reuse in MetroLog project
    // TODO GATH: Remove static and add to simpleIoc in boostrapper
    public static class PlatformAdapter
    {
        private static readonly ITracer tracer = Tracer.Create(typeof(PlatformAdapter));

        private static IAdapterResolver adapterResolver = new ProbingAdapterResolver(new DefaultRegistrationConvention());

        public static T Resolve<T>(bool throwIfNotFound = true, params object[] args)
        {
            tracer.Debug("Resolve with throwIfNotFound={0}", throwIfNotFound);
            T value = (T)adapterResolver.Resolve(typeof(T), args);

            if (value == null && throwIfNotFound)
            {
                string errorMessage = string.Format("PlatformNotSupportedException: Type {0} could not be resolved.", typeof(T).FullName);
                tracer.Error(errorMessage);
                throw new PlatformNotSupportedException(errorMessage);
            }

            return value;
        }

        // Unit testing helper
        internal static void SetResolver(IAdapterResolver resolver)
        {
            adapterResolver = resolver;
        }
    }
}