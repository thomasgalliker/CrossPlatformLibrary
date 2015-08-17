namespace CrossPlatformLibrary.ExceptionHandling
{
    public interface IPlatformSpecificExceptionHandler
    {
        void RegisterExceptionHandler(IExceptionHandler exceptionHandler);
    }
}