﻿using System;
using System.Text;
using System.Threading;

using CrossPlatformLibrary.ExceptionHandling.ExceptionHandlingStrategies;
using CrossPlatformLibrary.Internals;

namespace CrossPlatformLibrary.ExceptionHandling
{
    /// <summary>
    ///     AsyncSynchronizationContext helps handling unhandled exceptions with async/await.
    ///     Source:
    ///     http://www.markermetro.com/2013/01/technical/handling-unhandled-exceptions-with-asyncawait-on-windows-8-and-windows-phone-8/
    /// </summary>
    public class AsyncSynchronizationContext : SynchronizationContext
    {
        static AsyncSynchronizationContext()
        {
            tracer = Tracer.Create<AsyncSynchronizationContext>();
        }

        /// <summary>
        ///     Sets the SynchronisationContext for ui thread so that async void exceptions can be handled.
        ///     <example>
        ///         Example: Call this method with 'await' and observe the WrapCallback method:
        ///         private async void Test()
        ///         {
        ///         throw new Exception("TestException");
        ///         }
        ///     </example>
        /// </summary>
        /// <param name="strategy"></param>
        /// <returns></returns>
        public static AsyncSynchronizationContext Register(IExceptionHandlingStrategy strategy)
        {
            Guard.ArgumentNotNull(strategy, nameof(strategy));

            exceptionHandlingStrategy = strategy;

            var currentSynchronizationContext = SynchronizationContext.Current;
            if (currentSynchronizationContext == null)
            {
                if (strategy is RethrowExceptionHandlingStrategy == false)
                {
                    tracer.Info(new StringBuilder()
                        .Append("Ensure a synchronization context exists before calling AsyncSynchronizationContext.Register. ")
                        .Append("A synchronization context usually exists after the UI has been initialized. ")
                        .Append("If your application does not have a UI you can ignore this message.").ToString());
                }
       

                return null;
            }

            var customSynchronizationContext = currentSynchronizationContext as AsyncSynchronizationContext;
            if (customSynchronizationContext == null)
            {
                customSynchronizationContext = new AsyncSynchronizationContext(currentSynchronizationContext);
                SetSynchronizationContext(customSynchronizationContext);
            }

            return customSynchronizationContext;
        }

        private readonly SynchronizationContext syncContext;
        private static IExceptionHandlingStrategy exceptionHandlingStrategy;
        private static readonly ITracer tracer;

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
                        var isExceptionHandled = exceptionHandlingStrategy != null && exceptionHandlingStrategy.HandleException(ex);
                        if (!isExceptionHandled)
                        {
                            throw;
                        }
                    }
                };
        }
    }
}