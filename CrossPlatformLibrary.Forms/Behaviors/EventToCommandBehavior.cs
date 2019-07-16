﻿using System;
using System.Reflection;
using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class EventToCommandBehavior : BehaviorBase<View>
    {
        private Delegate eventHandler;

        public static readonly BindableProperty EventNameProperty = BindableProperty.Create(nameof(EventName), typeof(string), typeof(EventToCommandBehavior), null, propertyChanged: OnEventNameChanged);
        public static readonly BindableProperty CommandProperty = BindableProperty.Create(nameof(Command), typeof(ICommand), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty CommandParameterProperty = BindableProperty.Create(nameof(CommandParameter), typeof(object), typeof(EventToCommandBehavior), null);
        public static readonly BindableProperty InputConverterProperty = BindableProperty.Create(nameof(Converter), typeof(IValueConverter), typeof(EventToCommandBehavior), null);

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

        protected override void OnAttachedTo(View bindable)
        {
            base.OnAttachedTo(bindable);
            this.RegisterEvent(this.EventName);
        }

        protected override void OnDetachingFrom(View bindable)
        {
            base.OnDetachingFrom(bindable);
            this.DeregisterEvent(this.EventName);
        }

        private void RegisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            var eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException($"EventToCommandBehavior: Can't register the '{this.EventName}' event.");
            }

            var methodInfo = typeof(EventToCommandBehavior).GetTypeInfo().GetDeclaredMethod("OnEvent");
            this.eventHandler = methodInfo.CreateDelegate(eventInfo.EventHandlerType, this);
            eventInfo.AddEventHandler(this.AssociatedObject, this.eventHandler);
        }

        private void DeregisterEvent(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                return;
            }

            if (this.eventHandler == null)
            {
                return;
            }

            EventInfo eventInfo = this.AssociatedObject.GetType().GetRuntimeEvent(name);
            if (eventInfo == null)
            {
                throw new ArgumentException(string.Format("EventToCommandBehavior: Can't de-register the '{0}' event.", this.EventName));
            }

            eventInfo.RemoveEventHandler(this.AssociatedObject, this.eventHandler);
            this.eventHandler = null;
        }

        void OnEvent(object sender, object eventArgs)
        {
            if (this.Command == null)
            {
                return;
            }

            object resolvedParameter;
            if (this.CommandParameter != null)
            {
                resolvedParameter = this.CommandParameter;
            }
            else if (this.Converter != null)
            {
                resolvedParameter = this.Converter.Convert(eventArgs, typeof(object), null, null);
            }
            else
            {
                resolvedParameter = eventArgs;
            }

            if (this.Command.CanExecute(resolvedParameter))
            {
                this.Command.Execute(resolvedParameter);
            }
        }

        private static void OnEventNameChanged(BindableObject bindable, object oldValue, object newValue)
        {
            var behavior = (EventToCommandBehavior)bindable;
            if (behavior.AssociatedObject == null)
            {
                return;
            }

            string oldEventName = (string)oldValue;
            string newEventName = (string)newValue;

            behavior.DeregisterEvent(oldEventName);
            behavior.RegisterEvent(newEventName);
        }
    }
}