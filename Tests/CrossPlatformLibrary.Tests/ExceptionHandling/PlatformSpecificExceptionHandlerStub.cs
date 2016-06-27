
using System;

using CrossPlatformLibrary.ExceptionHandling;

namespace CrossPlatformLibrary.Tests.ExceptionHandling
{
    public class PlatformSpecificExceptionHandlerStub : IExceptionHandler
    {
        public bool HandleException(Exception exception)
        {
            return true;
        }
    }
}
