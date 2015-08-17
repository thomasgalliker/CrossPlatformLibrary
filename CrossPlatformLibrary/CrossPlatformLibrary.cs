
using System.Collections.Generic;

namespace CrossPlatformLibrary
{
    /// <summary>
    /// CrossPlatformLibrary is an extensible toolkit which provides a basic set of functionality 
    /// used in most mobile apps such as bootstrapping, exception handling, tracing and UI dispatching.
    /// </summary>
    public static class CrossPlatformLibrary
    {
        static CrossPlatformLibrary()
        {
            AssemblyNamespaces = new List<string> { Namespace };
        }

        private const string Namespace = "CrossPlatformLibrary";

        public static readonly List<string> AssemblyNamespaces;
    }
}
