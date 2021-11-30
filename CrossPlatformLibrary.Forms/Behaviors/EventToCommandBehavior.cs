using System;
using System.Reflection;
using System.Windows.Input;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<Xamarin.Forms.View>
    {
        private Delegate eventHandler;

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(
            nameof(EventName),
            typeof(string),
            typeof(EventToCommandBehavior),
            null,
            propertyChanged: OnEventNameChanged);

        public static readonly BindableProperty CommandProperty = BindableProperty.Create(
            nameof(Command),
            typeof(ICommand),
            typeof(EventToCommandBehavior),
            null);

        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(
            nameof(CommandParameter),
            typeof(object),
            typeof(EventToCommandBehavior),
            null);

        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(
            nameof(Converter),
            typeof(IValueConverter),
            typeof(EventToCommandBehavior),
            null);

        public string EventName
        {
            get => (string)this.GetValue(EventNameProperty);
            set => this.SetValue(EventNameProperty, value);
        }

        public ICommand Command
        {
            get => (ICommand)this.GetValue(CommandProperty);
            set => this.SetValue(CommandProperty, value);
        }

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public IValueConverter Converter
        {
            get => (IValueConverter)this.GetValue(InputConverterProperty);
            set => this.SetValue(InputConverterProperty, value);
        }

        protected override void OnAttachedTo(Xamarin.Forms.View bindable)
        {
            base.OnAttachedTo(bindable);
            this.RegisterEvent(this.EventName);
        }

        protected override void OnDetachingFrom(Xamarin.Forms.View bindable)
        {
            this.DeregisterEvent(this.EventName);
            base.OnDetachingFrom(bindable);
        }

        private void RegisterEvent(string name)
        {
            Tracer.Current.Debug($"EventToCommandBehavior.RegisterEvent(name={name})");

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{this.EventName}' event.");
            }

            var paramType = eventInfo.EventHandlerType.IsGenericType
                ? eventInfo.EventHandlerType.GetGenericArguments()
                : (new[] { typeof(object) });

            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo()
                .GetDeclaredMethod(nameof(this.OnEvent))
                .MakeGenericMethod(paramType);

            this.eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(this.AssociatedObject, this.eventHandler);
        }

        private void DeregisterEvent(string name)
        {
            Tracer.Current.Debug($"EventToCommandBehavior.DeregisterEvent(name={name})");

            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (this.eventHandler == null)
            {
                return;
            }

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBehavior: Can't de-register the '{this.EventName}' event.");
            }

            eventInfo.RemoveEventHandler(this.AssociatedObject, this.eventHandler);
            this.eventHandler = null;
        }

        private void OnEvent<T>(object sender, T eventArgs)
        {
            Tracer.Current.Debug($"EventToCommandBehavior.OnEvent(sender={sender?.GetType().GetFormattedName() ?? "<null>"}, eventArgs={eventArgs?.GetType().GetFormattedName() ?? "<null>"})");

            if (!(this.Command is ICommand command))
            {
                return;
            }

            // Don't call the Command in case the binding context is set to null
            if (this.AssociatedObject?.BindingContext == null)
            {
                return;
            }

            object resolvedParameter;
            if (this.CommandParameter is object commandParameter)
            {
                resolvedParameter = commandParameter;
            }
            else if (this.Converter != null)
            {
                resolvedParameter = this.Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (command.CanExecute(resolvedParameter))
            {
                command.Execute(resolvedParameter);
            }
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociatedObject == null)
            {
                return;
            }

            var oldEventName = (string)oldValue;
            var newEventName = (string)newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}
