using System;

namespace CrossPlatformLibrary.Bootstrapping
{
    public class ApplicationLifecycleException : Exception
    {
        public ApplicationLifecycleException()
        {
        }

        public ApplicationLifecycleException(string message)
            : base(message)
        {
        }

        public ApplicationLifecycleException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        public static ApplicationLifecycleException InvalidStateTransition(ApplicationLifecycle current, ApplicationLifecycle target)
        {
            return new ApplicationLifecycleException(string.Format("Invalid application lifecycle state transition: Cannot change from {0} to {1}.", current, target));
        }
    }
}