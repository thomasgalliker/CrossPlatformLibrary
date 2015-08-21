using System;

namespace CrossPlatformLibrary.Dispatching
{
    public interface IDispatcherService
    {
        ////void BeginInvokeOnUI(Action action); //TODO GATH: Implement non-checking version of beginInvoke

        /// <summary>
        ///     Executes an action on the UI thread. If this method is called
        ///     from the UI thread, the action is executed immendiately. If the
        ///     method is called from another thread, the action will be enqueued
        ///     on the UI thread's dispatcher and executed asynchronously.
        ///     <para>
        ///         For additional operations on the UI thread, you can get a
        ///         reference to the UI thread's dispatcher thanks to the property
        ///         <see cref="DispatcherService.UIDispatcher" />
        ///     </para>
        ///     .
        /// </summary>
        /// <param name="action">
        ///     The action that will be executed on the UI
        ///     thread.
        /// </param>
        void CheckBeginInvokeOnUI(Action action);
    }
}