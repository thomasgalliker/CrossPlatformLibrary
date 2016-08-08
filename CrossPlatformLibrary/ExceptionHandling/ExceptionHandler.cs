namespace CrossPlatformLibrary.ExceptionHandling
{
    public class ExceptionHandler : IExceptionHandler
    {
        public ExceptionHandler()
        {
            throw new NotImplementedInReferenceAssemblyException();
        }
    }
}
