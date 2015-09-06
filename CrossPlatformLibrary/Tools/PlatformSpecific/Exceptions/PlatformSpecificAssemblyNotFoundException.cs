using System;

namespace CrossPlatformLibrary.Tools.PlatformSpecific.Exceptions
{
    public class PlatformSpecificAssemblyNotFoundException : Exception
    {
        public PlatformSpecificAssemblyNotFoundException()
        {
        }
    
        public PlatformSpecificAssemblyNotFoundException(string message)
            : base(message)
        {
        }

        public PlatformSpecificAssemblyNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
