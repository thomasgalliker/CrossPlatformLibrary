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
        public Task<T> RunWithDelay<T>(TimeSpan delay, Func<Task<T>> task, Func<T> defaultValue)
        {
            var tcs = new TaskCompletionSource<T>();

            Task.Factory.StartNew(async () =>
            {
                try
                {
                    Interlocked.Exchange(ref this.throttleCts, new CancellationTokenSource()).Cancel();
                    await Task.Delay(delay, this.throttleCts.Token)
                        .ContinueWith(async ct =>
                            {
                                var result = await task().ConfigureAwait(false);
                                tcs.TrySetResult(result);
                            },
                            CancellationToken.None,
                            TaskContinuationOptions.OnlyOnRanToCompletion,
                            TaskScheduler.Default);
                }
                catch
                {
                    // Ignore any Threading errors
                    tcs.TrySetResult(defaultValue());
                }
            }).ConfigureAwait(false);

            return tcs.Task;
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
                    .ContinueWith(async ct => await task().ConfigureAwait(false),
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