using System;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class BehaviorBase<T> : Behavior<T> where T : BindableObject
    {
        public T AssociatedObject { get; private set; }

        protected override void OnAttachedTo(T bindable)
        {
            base.OnAttachedTo(bindable);
            this.AssociatedObject = bindable;

            if (bindable.BindingContext != null)
            {
                this.BindingContext = bindable.BindingContext;
            }

            bindable.BindingContextChanged += this.OnBindingContextChanged;
        }

        protected override void OnDetachingFrom(T bindable)
        {
            base.OnDetachingFrom(bindable);
            bindable.BindingContextChanged -= this.OnBindingContextChanged;
            this.AssociatedObject = null;
        }

        private void OnBindingContextChanged(object sender, EventArgs e)
        {
            base.OnBindingContextChanged();

            if (!(sender is BindableObject bindableObject))
            {
                return;
            }

            // Assumption: In case the BindingContext is set to null,
            //             we detach the object from the behavior.
            if (bindableObject.BindingContext == null)
            {
                this.OnDetachingFrom(bindableObject);
            }

            this.BindingContext = bindableObject.BindingContext;
        }
    }
}


