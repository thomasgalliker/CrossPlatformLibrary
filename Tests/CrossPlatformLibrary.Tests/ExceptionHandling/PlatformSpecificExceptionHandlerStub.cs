
using CrossPlatformLibrary.ExceptionHandling;

namespace CrossPlatformLibrary.Tests.ExceptionHandling
{
    public class PlatformSpecificExceptionHandlerStub : IPlatformSpecificExceptionHandler
    {
        public void RegisterExceptionHandler(IExceptionHandler exceptionHandler)
        {
        }
    }
}
