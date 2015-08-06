
using CrossPlatformLibrary.Tools.PlatformSpecific;

namespace CrossPlatformLibrary.IoC
{
    public static class ISimpleIocExtensions
    {
        /// <summary>
        ///     Registers a given platform-specific instance for a given platform-independent interface type.
        /// </summary>
        /// <typeparam name="TInterface">The platform-independent interface type that is being registered.</typeparam>
        public static void RegisterPlatformSpecific<TInterface>(this ISimpleIoc container, bool createInstanceImmediately = false) where TInterface : class
        {
            container.Register<TInterface>(() => PlatformAdapter.Resolve<TInterface>(), createInstanceImmediately);
        }
    }
}
