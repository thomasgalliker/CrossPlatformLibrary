using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Tests.Testdata
{
    internal class TestListView : TestListView<object>
    {
    }

    internal class TestListView<T> : ListView
    {
        public virtual event EventHandler EventWithoutArgs;

        public void RaiseEventWithoutArgs()
        {
            EventWithoutArgs?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<T> EventWithEventArgs;

        public void RaiseEventWithEventArgs(T eventArgs)
        {
            EventWithEventArgs?.Invoke(this, eventArgs);
        }
    }
}
