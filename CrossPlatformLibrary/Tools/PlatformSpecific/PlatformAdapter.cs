using System;

using CrossPlatformLibrary.Tracing;

namespace CrossPlatformLibrary.Tools.PlatformSpecific
{
    /// <summary>
    ///     Enables types within PclContrib to use platform-specific features in a platform-agnostic way
    /// </summary>
    public static class PlatformAdapter // TODO GATH: Change to singleton! Make it more configurable
    {
        private static readonly ITracer tracer = Tracer.Create(typeof(PlatformAdapter));
        private static IAdapterResolver adapterResolver = new ProbingAdapterResolver();

        public static T Resolve<T>(bool throwIfNotFound = true, params object[] args)
        {
            tracer.Debug("Resolve with throwIfNotFound={0}", throwIfNotFound);
            T value = (T)adapterResolver.Resolve(typeof(T), args);

            if (value == null && throwIfNotFound)
            {
                tracer.Error("PlatformNotSupportedException: Type {0} not found", typeof(T).FullName);
                throw new PlatformNotSupportedException("AdapterNotSupported");
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