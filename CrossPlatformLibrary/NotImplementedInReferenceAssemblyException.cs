using System;
using System.Runtime.CompilerServices;

namespace CrossPlatformLibrary
{
    /// <summary>
    /// This exception is raise when a platform-agnostic function is called from a specific platform.
    /// Try to reinstall the nuget package of this project and run the code again.
    /// </summary>
    public class NotImplementedInReferenceAssemblyException : NotImplementedException
    {
        public NotImplementedInReferenceAssemblyException([CallerMemberName] string caller = null) : base(
                 $"Method '{caller}' is not implemented in the portable version of this assembly. "
                + "You should reference the NuGet package from your main application project in order to reference the platform-specific implementation.")
        {
        }
    }
}
