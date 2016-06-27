using System.Reflection;

namespace CrossPlatformLibrary.Tools
{
    /// <summary>
    /// Provides general platform services.
    /// </summary>
    public interface IPlatformServices
    {
        /// <summary>
        /// Returns a list of all loaded assemblies.
        /// </summary>
        /// <returns></returns>
        Assembly[] GetAssemblies();

        /// <summary>
        /// LoadReferencedAssemblies makes sure that all assemblies which are referenced
        /// are also loaded.
        /// </summary>
        void LoadReferencedAssemblies();
    }
}