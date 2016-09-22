using System;
using System.Threading.Tasks;

namespace Sample
{
    public static class ExceptionHandlingDemo
    {
        public static void ThrowApplicationException()
        {
            throw new InvalidOperationException("Some exception text...");
        }

        public static void ThrowUnobservedTaskException()
        {
            Task.Factory.StartNew(() =>
            {
                throw new InvalidOperationException("Some unobserved task exception text...");
            });

            Task.Delay(2000).ContinueWith(
                ct =>
                {
                    // We need to enforce GC manually
                    // so that the GC pushed the unobserved task exceptions to
                    // ExceptionHandlerBase.OnTaskSchedulerUnobservedTaskException
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                });
        }
    }
}
