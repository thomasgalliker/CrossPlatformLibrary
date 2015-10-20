using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

public static class AsyncInline
{
    public static void Run(Func<Task> item)
    {
        var oldContext = SynchronizationContext.Current;
        var synch = new ExclusiveSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(synch);
        synch.Post(
            async _ =>
                {
                    try
                    {
                        await item();
                    }
                    catch (Exception e)
                    {
                        synch.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        synch.EndMessageLoop();
                    }
                },
            null);
        synch.BeginMessageLoop();
        SynchronizationContext.SetSynchronizationContext(oldContext);
    }

    public static T Run<T>(Func<Task<T>> item)
    {
        var oldContext = SynchronizationContext.Current;
        var synch = new ExclusiveSynchronizationContext();
        SynchronizationContext.SetSynchronizationContext(synch);
        T ret = default(T);
        synch.Post(
            async _ =>
                {
                    try
                    {
                        ret = await item();
                    }
                    catch (Exception e)
                    {
                        synch.InnerException = e;
                        throw;
                    }
                    finally
                    {
                        synch.EndMessageLoop();
                    }
                },
            null);
        synch.BeginMessageLoop();
        SynchronizationContext.SetSynchronizationContext(oldContext);
        return ret;
    }

    private class ExclusiveSynchronizationContext : SynchronizationContext
    {
        private bool done;

        public Exception InnerException { get; set; }

        private readonly AutoResetEvent workItemsWaiting = new AutoResetEvent(false);
        private readonly Queue<Tuple<SendOrPostCallback, object>> items = new Queue<Tuple<SendOrPostCallback, object>>();

        public override void Send(SendOrPostCallback d, object state)
        {
            throw new NotSupportedException("We cannot send to our same thread");
        }

        public override void Post(SendOrPostCallback d, object state)
        {
            lock (this.items)
            {
                this.items.Enqueue(Tuple.Create(d, state));
            }
            this.workItemsWaiting.Set();
        }

        public void EndMessageLoop()
        {
            this.Post(_ => this.done = true, null);
        }

        public void BeginMessageLoop()
        {
            while (!this.done)
            {
                Tuple<SendOrPostCallback, object> task = null;
                lock (this.items)
                {
                    if (this.items.Count > 0)
                    {
                        task = this.items.Dequeue();
                    }
                }
                if (task != null)
                {
                    task.Item1(task.Item2);
                    if (this.InnerException != null) // the method threw an exeption
                    {
                        throw new AggregateException("AsyncInline.Run method threw an exception.", this.InnerException);
                    }
                }
                else
                {
                    this.workItemsWaiting.WaitOne();
                }
            }
        }

        public override SynchronizationContext CreateCopy()
        {
            return this;
        }
    }
}