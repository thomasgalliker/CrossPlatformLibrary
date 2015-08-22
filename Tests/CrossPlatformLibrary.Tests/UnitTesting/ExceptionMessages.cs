namespace CrossPlatformLibrary.Tests.UnitTesting
{
    internal static class ExceptionMessages
    {
        internal const string ExceptionAssertExceptionNotThrown = "Expected exception of type '{0}' has not been thrown.";
        internal const string ExceptionAssertExceptionThrown = "Not expected exception of type '{0}' has been thrown.";
        internal const string ExceptionAssertWrongException = "Thrown exception is instance of type '{0}', but not instance of type {1}.'";
    }
}