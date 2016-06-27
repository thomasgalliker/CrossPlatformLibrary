using System;
using System.Runtime.CompilerServices;

namespace CrossPlatformLibrary
{
    internal static class Exceptions
    {
        internal static Exception NotImplementedInReferenceAssembly([CallerMemberName] string caller = null)
        {
            return new NotImplementedException(
                 $"Method '{caller}' is not implemented in the portable version of this assembly. "
                + "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.");
        }
    }
}
