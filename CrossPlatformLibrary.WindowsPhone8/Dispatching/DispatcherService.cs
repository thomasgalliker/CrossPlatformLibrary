using System;
using System.Windows;
using System.Windows.Threading;

using CrossPlatformLibrary.Utils;

#if NETFX_CORE
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.Foundation;
#else
#if SILVERLIGHT

#endif
#endif

namespace CrossPlatformLibrary.Dispatching
{
    public class DispatcherService : IDispatcherService
    {
        public DispatcherService()
        {
            this.Initialize();
        }

        /// <summary>
        ///     Gets a reference to the UI thread's dispatcher, after the
        ///     <see cref="Initialize" /> method has been called on the UI thread.
        /// </summary>
#if NETFX_CORE
        public CoreDispatcher UIDispatcher
#else
        public Dispatcher UIDispatcher
#endif
        { get; private set; }

        /// <summary>
        ///     Executes an action on the UI thread. If this method is called
        ///     from the UI thread, the action is executed immendiately. If the
        ///     method is called from another thread, the action will be enqueued
        ///     on the UI thread's dispatcher and executed asynchronously.
        ///     <para>
        ///         For additional operations on the UI thread, you can get a
        ///         reference to the UI thread's dispatcher thanks to the property
        ///         <see cref="UIDispatcher" />
        ///     </para>
        ///     .
        /// </summary>
        /// <param name="action">
        ///     The action that will be executed on the UI
        ///     thread.
        /// </param>
        public void CheckBeginInvokeOnUI(Action action)
        {
            Guard.ArgumentNotNull(() => action);

#if NETFX_CORE
            if (UIDispatcher.HasThreadAccess)
#else
            if (this.UIDispatcher.CheckAccess())
#endif
            {
                action();
            }
            else
            {
#if NETFX_CORE
                UIDispatcher.RunAsync(CoreDispatcherPriority.Normal,  () => action());
#else
                this.UIDispatcher.BeginInvoke(action);
#endif
            }
        }

#if NETFX_CORE
    /// <summary>
    /// Invokes an action asynchronously on the UI thread.
    /// </summary>
    /// <param name="action">The action that must be executed.</param>
    /// <returns>The object that provides handlers for the completed async event dispatch.</returns>
        public static IAsyncAction RunAsync(Action action)
#else
        /// <summary>
        ///     Invokes an action asynchronously on the UI thread.
        /// </summary>
        /// <param name="action">The action that must be executed.</param>
        /// <returns>
        ///     An object, which is returned immediately after BeginInvoke is called, that can be used to interact
        ///     with the delegate as it is pending execution in the event queue.
        /// </returns>
        public DispatcherOperation RunAsync(Action action)
#endif
        {
#if NETFX_CORE
            return UIDispatcher.RunAsync(CoreDispatcherPriority.Normal, () => action());
#else
            return this.UIDispatcher.BeginInvoke(action);
#endif
        }

        /// <summary>
        ///     This method should be called once on the UI thread to ensure that
        ///     the <see cref="UIDispatcher" /> property is initialized.
        ///     <para>
        ///         In a Silverlight application, call this method in the
        ///         Application_Startup event handler, after the MainPage is constructed.
        ///     </para>
        ///     <para>In WPF, call this method on the static App() constructor.</para>
        /// </summary>
        private void Initialize()
        {
#if SILVERLIGHT
            if (this.UIDispatcher != null)
#else
#if NETFX_CORE
            if (UIDispatcher != null)
#else
            if (UIDispatcher != null
                && UIDispatcher.Thread.IsAlive)
#endif
#endif
            {
                return;
            }

#if NETFX_CORE
            UIDispatcher = Window.Current.Dispatcher;
#else
#if SILVERLIGHT
            this.UIDispatcher = Deployment.Current.Dispatcher;
#else
            UIDispatcher = Dispatcher.CurrentDispatcher;
#endif
#endif
        }

        /// <summary>
        ///     Resets the class by deleting the <see cref="UIDispatcher" />
        /// </summary>
        public void Reset()
        {
            this.UIDispatcher = null;
        }
    }
}