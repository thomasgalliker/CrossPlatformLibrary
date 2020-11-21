using System;
using System.Diagnostics;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Extensions
{
    public static class MessagingCenterExtensions
    {
        /// <summary>
        /// Sends a named <paramref name="message" /> without arguments.
        /// </summary>
        public static void Send(this IMessagingCenter messagingCenter, string message)
        {
            var subscription = new Subscription<object>(messagingCenter, message);
            messagingCenter.Send<ISubscription, object>(subscription, message, null);
        }

        /// <summary>
        /// Sends a named <paramref name="message" /> with arguments of type <typeparam name="TArgs"/>.
        /// </summary>
        public static void Send<TArgs>(this IMessagingCenter messagingCenter, string message, TArgs args)
        {
            var subscription = new Subscription<TArgs>(messagingCenter, message);
            messagingCenter.Send<ISubscription, TArgs>(subscription, message, args);
        }

        /// <summary>
        /// Subscribe to a named <paramref name="message" /> and run the <paramref name="callback" /> if the event is fired.
        /// </summary>
        /// <returns>Subscription which can be used to unsubscribe from the event.</returns>
        public static ISubscription Subscribe(this IMessagingCenter messagingCenter, string message, Action callback)
        {
            var subscription = new Subscription<object>(messagingCenter, message);
            messagingCenter.Subscribe<ISubscription, object>(subscription, message, (s, args) => callback());
            return subscription;
        }

        /// <summary>
        /// Subscribe to a named <paramref name="message" /> and run the <paramref name="callback" /> with parameter <typeparam name="TArgs"/> if the event is fired.
        /// </summary>
        /// <returns>Subscription which can be used to unsubscribe from the event.</returns>
        public static ISubscription Subscribe<TArgs>(this IMessagingCenter messagingCenter, string message, Action<TArgs> callback)
        {
            var subscription = new Subscription<TArgs>(messagingCenter, message);
            messagingCenter.Subscribe<ISubscription, TArgs>(subscription, message, (s, args) => callback(args));
            return subscription;
        }
    }

    [DebuggerDisplay("Subscription: Id={this.Id}, Message={this.message}")]
    internal class Subscription<TArgs> : ISubscription
    {
        private readonly IMessagingCenter messagingCenter;
        private readonly string message;

        internal Subscription(IMessagingCenter messagingCenter, string message)
        {
#if DEBUG
            this.Id = Guid.NewGuid().ToString("D");
#endif
            this.messagingCenter = messagingCenter;
            this.message = message;
        }

#if DEBUG
        public string Id { get; set; }
#endif

        public void Unsubscribe()
        {
            this.messagingCenter.Unsubscribe<ISubscription, TArgs>(this, this.message);
        }
    }

    /// <summary>
    /// Event subscription.
    /// </summary>
    public interface ISubscription
    {
        /// <summary>
        /// Unsubscribe from the current event subscription.
        /// </summary>
        void Unsubscribe();
    }
}