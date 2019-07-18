using System;
using System.Threading;
using System.Threading.Tasks;

namespace CrossPlatformLibrary
{
    public class TaskDelayer
    {
        private CancellationTokenSource throttleCts = new CancellationTokenSource();

        public TaskDelayer()
        {
        }

        /// <summary>
        /// Runs the given <paramref name="action"/> in a background thread with a sliding <paramref name="delay"/>.
        /// </summary>
        public Task RunWithDelay(TimeSpan delay, Action action)
        {
            return this.RunWithDelay(delay, () =>
            {
                action();
#if (NETFX)
                return Task.FromResult(false);
#else
                return Task.CompletedTask;
#endif

            });
        }

        /// <summary>
        /// Runs the given <paramref name="task"/> in a background thread with a sliding <paramref name="delay"/>.
        /// </summary>
        public async Task RunWithDelay(TimeSpan delay, Func<Task> task)
        {
            try
            {
                Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
                await Task.Delay(delay, this.throttleCts.Token)
                    .ContinueWith(async ct => await task(),
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());
            }
            catch
            {
                // Ignore any Threading errors
            }
        }
    }
}