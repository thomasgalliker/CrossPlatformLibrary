﻿using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.Utils;
#if __UNIFIED__
using Foundation;
#else
using MonoTouch.Foundation;
#endif

namespace Xamarin.ExceptionHandling
{
    public class PlatformSpecificExceptionHandler : IPlatformSpecificExceptionHandler
    {
        private const string StackTraceDirectory = "stacktraces";
        private IExceptionHandler exceptionHandler;

        [DllImport("libc")]
        private static extern int sigaction(Signal sig, IntPtr act, IntPtr oact);

        enum Signal
        {
            SIGBUS = 10,
            SIGSEGV = 11
        }

        public void Attach(IExceptionHandler handler)
        {
            Guard.ArgumentNotNull(() => handler);

            Interlocked.Exchange(ref this.exceptionHandler, handler);

            this.Detach();

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

        public void Detach()
        {
            AppDomain.CurrentDomain.UnhandledException -= this.CurrentDomainUnhandledException;
        }

        private void CurrentDomainUnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            var exception = e.ExceptionObject as Exception;
            if (exception != null)
            {
                this.exceptionHandler.HandleException(exception);
            }
        }
    }
}
