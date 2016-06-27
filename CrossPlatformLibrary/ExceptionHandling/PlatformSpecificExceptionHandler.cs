namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : IExceptionHandler
    {
        public PlatformSpecificExceptionHandler()
        {
            throw Exceptions.NotImplementedInReferenceAssembly();
        }
    }
}
