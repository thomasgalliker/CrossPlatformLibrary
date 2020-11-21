using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;

namespace NeoSmart.AsyncLock
{
    public class AsyncLock
    {
        private readonly object reentrancy = new object();

        private int reentrances = 0;

        //We are using this SemaphoreSlim like a posix condition variable
        //we only want to wake waiters, one or more of whom will try to obtain a different lock to do their thing
        //so long as we can guarantee no wakes are missed, the number of awakees is not important
        //ideally, this would be "friend" for access only from InnerLock, but whatever.
        internal SemaphoreSlim Retry = new SemaphoreSlim(0, 1);

        //We do not have System.Threading.Thread.* on .NET Standard without additional dependencies
        //Work around is easy: create a new ThreadLocal<T> with a random value and this is our thread id :)
        private static readonly long UnlockedThreadId = 0; //"owning" thread id when unlocked
        internal long OwningId = UnlockedThreadId;
        private static int globalThreadCounter;

        private static readonly ThreadLocal<int> threadId = new ThreadLocal<int>(() => Interlocked.Increment(ref globalThreadCounter));

        //We generate a unique id from the thread ID combined with the task ID, if any
        public static long ThreadId => (long)(((ulong)threadId.Value) << 32) | ((uint)(Task.CurrentId ?? 0));

        private struct InnerLock : IDisposable
        {
            private readonly AsyncLock parent;
#if DEBUG
            private bool disposed;
#endif

            internal InnerLock(AsyncLock parent)
            {
                this.parent = parent;
#if DEBUG
                this.disposed = false;
#endif
            }

            internal async Task ObtainLockAsync()
            {
                while (!this.TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    await this.parent.Retry.WaitAsync();
                }
            }

            internal async Task ObtainLockAsync(CancellationToken ct)
            {
                while (!this.TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    await this.parent.Retry.WaitAsync(ct);
                }
            }

            internal async Task ObtainLockAsync(TimeSpan timeout)
            {
                while (!this.TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    await this.parent.Retry.WaitAsync(timeout);
                }
            }

            internal void ObtainLock()
            {
                while (!this.TryEnter())
                {
                    //we need to wait for someone to leave the lock before trying again
                    this.parent.Retry.Wait();
                }
            }

            private bool TryEnter()
            {
                lock (this.parent.reentrancy)
                {
                    Debug.Assert((this.parent.OwningId == UnlockedThreadId) == (this.parent.reentrances == 0));
                    if (this.parent.OwningId != UnlockedThreadId && this.parent.OwningId != ThreadId)
                    {
                        //another thread currently owns the lock
                        return false;
                    }

                    //we can go in
                    Interlocked.Increment(ref this.parent.reentrances);
                    this.parent.OwningId = ThreadId;
                    return true;
                }
            }

            public void Dispose()
            {
#if DEBUG
                Debug.Assert(!this.disposed);
                this.disposed = true;
#endif
                lock (this.parent.reentrancy)
                {
                    Interlocked.Decrement(ref this.parent.reentrances);
                    if (this.parent.reentrances == 0)
                    {
                        //the owning thread is always the same so long as we are in a nested stack call
                        //we reset the owning id to null only when the lock is fully unlocked
                        this.parent.OwningId = UnlockedThreadId;
                        if (this.parent.Retry.CurrentCount == 0)
                        {
                            this.parent.Retry.Release();
                        }
                    }
                }
            }
        }

        public IDisposable Lock()
        {
            var @lock = new InnerLock(this);
            @lock.ObtainLock();
            return @lock;
        }

        public async Task<IDisposable> LockAsync()
        {
            var @lock = new InnerLock(this);
            await @lock.ObtainLockAsync();
            return @lock;
        }

        public async Task<IDisposable> LockAsync(CancellationToken ct)
        {
            var @lock = new InnerLock(this);
            await @lock.ObtainLockAsync(ct);
            return @lock;
        }

        public async Task<IDisposable> LockAsync(TimeSpan timeout)
        {
            var @lock = new InnerLock(this);
            await @lock.ObtainLockAsync(timeout);
            return @lock;
        }
    }
}