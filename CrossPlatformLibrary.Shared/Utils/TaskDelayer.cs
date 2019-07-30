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
        ///     Runs the given <paramref name="action" /> in a background thread with a sliding <paramref name="delay" />.
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
        ///     Runs the given <paramref name="task" /> in a background thread with a sliding <paramref name="delay" />.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="delay">Sliding delay.</param>
        /// <param name="task">The task to be executed after delay.</param>
        /// <param name="defaultValue">The default value to be returned in case of a cancellation</param>
        /// <returns></returns>
        public async Task<T> RunWithDelay<T>(TimeSpan delay, Func<Task<T>> task, Func<T> defaultValue)
        {
            try
            {
                Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
                var result = default(T);
                await Task.Delay(delay, this.throttleCts.Token)
                    .ContinueWith(async ct => { result = await task(); },
                        CancellationToken.None,
                        TaskContinuationOptions.OnlyOnRanToCompletion,
                        TaskScheduler.FromCurrentSynchronizationContext());

                return result;
            }
            catch /*(Exception ex)*/
            {
                // Ignore any Threading errors
            }

            return defaultValue();
        }

        /// <summary>
        ///     Runs the given <paramref name="task" /> in a background thread with a sliding <paramref name="delay" />.
        /// </summary>
        /// <param name="delay">Sliding delay.</param>
        /// <param name="task">The task to be executed after delay.</param>
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