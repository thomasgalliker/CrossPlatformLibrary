using System;
using System.IO;
using System.Runtime.InteropServices;
#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

namespace CrossPlatformLibrary.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : ExceptionHandlerBase
    {
        private const string StackTraceDirectory = "stacktraces";

        [DllImport("libc")]
        private static extern int sigaction(Signal sig, IntPtr act, IntPtr oact);

        enum Signal
        {
            SIGBUS = 10,
            SIGSEGV = 11
        }

        public PlatformSpecificExceptionHandler(IExceptionHandlingStrategy exceptionHandlingStrategy)
            : base(exceptionHandlingStrategy)
        {
        }

        protected override void Attach()
        {
            AppDomain.CurrentDomain.UnhandledException += this.CurrentDomainUnhandledException;

            this.AttachToNativeExceptions(true, true);
        }

        private void AttachToNativeExceptions(bool canReportNativeErrors, bool hijackNativeSignals)
        {
            if (canReportNativeErrors)
            {
                if (hijackNativeSignals)
                {
                    IntPtr sigbus = Marshal.AllocHGlobal(512);
                    IntPtr sigsegv = Marshal.AllocHGlobal(512);

                    // Store Mono SIGSEGV and SIGBUS handlers
                    sigaction(Signal.SIGBUS, IntPtr.Zero, sigbus);
                    sigaction(Signal.SIGSEGV, IntPtr.Zero, sigsegv);

                    ////_client._reporter = Mindscape.Raygun4Net.Xamarin.iOS.Raygun.SharedReporterWithApiKey(apiKey);

                    // Restore Mono SIGSEGV and SIGBUS handlers
                    sigaction(Signal.SIGBUS, sigbus, IntPtr.Zero);
                    sigaction(Signal.SIGSEGV, sigsegv, IntPtr.Zero);

                    Marshal.FreeHGlobal(sigbus);
                    Marshal.FreeHGlobal(sigsegv);
                }
                else
                {
                    ////_client._reporter = Mindscape.Raygun4Net.Xamarin.iOS.Raygun.SharedReporterWithApiKey(apiKey);
                }
            }

            ////_client.User = user; // Set this last so that it can be passed to the native reporter.

            ////string deviceId = _client.DeviceId;
            ////if (user == null && _client._reporter != null && !String.IsNullOrWhiteSpace(deviceId))
            ////{
            ////    _client._reporter.Identify(deviceId);
            ////}
        }

        private static string StackTracePath
        {
            get
            {
                string documents = NSFileManager.DefaultManager.GetUrls(NSSearchPathDirectory.DocumentDirectory, NSSearchPathDomain.User)[0].Path;
                var path = Path.Combine(documents, "..", "Library", "Caches", StackTraceDirectory);
                return path;
            }
        }

        protected override void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.HandleException(exception);
            }
        }
    }
}
