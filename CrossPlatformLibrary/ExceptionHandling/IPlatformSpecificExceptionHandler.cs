namespace CrossPlatformLibrary.ExceptionHandling
{
    public interface IPlatformSpecificExceptionHandler
    {
        void Attach(IExceptionHandler exceptionHandler);

        void Detach();
    }
}