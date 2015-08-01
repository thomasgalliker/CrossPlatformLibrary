﻿using System;
using System.Threading;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    ///     AsyncSynchronizationContext helps handling unhandled exceptions with async/await.
    ///     Source:
    ///     http://www.markermetro.com/2013/01/technical/handling-unhandled-exceptions-with-asyncawait-on-windows-8-and-windows-phone-8/
    /// </summary>
    public class AsyncSynchronizationContext : SynchronizationContext
    {
        public static AsyncSynchronizationContext Register(IExceptionHandler handler)
        {
            exceptionHandler = handler;

            var currentSyncContext = Current;
            if (currentSyncContext == null)
            {
                throw new InvalidOperationException("Ensure a synchronization context exists before calling this method.");
            }

            var customSynchronizationContext = currentSyncContext as AsyncSynchronizationContext;

            if (customSynchronizationContext == null)
            {
                customSynchronizationContext = new AsyncSynchronizationContext(currentSyncContext);
                SetSynchronizationContext(customSynchronizationContext);
            }

            return customSynchronizationContext;
        }

        private readonly SynchronizationContext syncContext;
        private static IExceptionHandler exceptionHandler;

        private AsyncSynchronizationContext(SynchronizationContext syncContext)
        {
            this.syncContext = syncContext;
        }

        public override SynchronizationContext CreateCopy()
        {
            return new AsyncSynchronizationContext(this.syncContext.CreateCopy());
        }

        public override void OperationCompleted()
        {
            this.syncContext.OperationCompleted();
        }

        public override void OperationStarted()
        {
            this.syncContext.OperationStarted();
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            this.syncContext.Post(WrapCallback(d), state);
        }

        public override void Send(SendOrPostCallback d, object state)
        {
            this.syncContext.Send(d, state);
        }

        private static SendOrPostCallback WrapCallback(SendOrPostCallback sendOrPostCallback)
        {
            return state =>
                {
                    try
                    {
                        sendOrPostCallback(state);
                    }
                    catch (Exception ex)
                    {
                        exceptionHandler.HandleException(ex);
                    }
                };
        }
    }
}