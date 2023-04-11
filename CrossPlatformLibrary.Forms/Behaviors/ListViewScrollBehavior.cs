using System;
using System.ComponentModel;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class ListViewScrollBehavior : BehaviorBase<ListView>
    {
        private bool isRendered = false;

        protected override void OnAttachedTo(ListView bindable)
        {
            base.OnAttachedTo(bindable);

            this.AssociatedObject.PropertyChanged += this.OnListViewPropertyChanged;
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            this.AssociatedObject.PropertyChanged -= this.OnListViewPropertyChanged;

            base.OnDetachingFrom(bindable);
        }

        private void OnListViewPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (this.isRendered == false && e.PropertyName == nameof(ListView.Height))
            {
                this.isRendered = true;

                // If the underlying ListView control is rendered AND the binding properties have valid values,
                // we have to issue a ScrollTo command
                this.ScrollToOnMainThread(this.TargetElement, this.ScrollToPosition);
            }
        }

        public static readonly BindableProperty ScrollToPositionProperty =
            BindableProperty.Create(
               nameof(ScrollToPosition),
               typeof(ScrollToPosition),
               typeof(ListViewScrollBehavior),
               null,
               propertyChanged: OnScrollToPositionPropertyChanged);

        public ScrollToPosition? ScrollToPosition
        {
            get => (ScrollToPosition?)this.GetValue(ScrollToPositionProperty);
            set => this.SetValue(ScrollToPositionProperty, value);
        }

        private static void OnScrollToPositionPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ListViewScrollBehavior behavior))
            {
                return;
            }

            behavior.ScrollToOnMainThread(behavior.TargetElement, newValue as ScrollToPosition?);
        }

        public static readonly BindableProperty TargetElementProperty =
            BindableProperty.Create(
               nameof(TargetElement),
               typeof(object),
               typeof(ListViewScrollBehavior),
               null,
               propertyChanged: OnTargetElementPropertyChanged);

        public object TargetElement
        {
            get => this.GetValue(TargetElementProperty);
            set => this.SetValue(TargetElementProperty, value);
        }

        private static void OnTargetElementPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ListViewScrollBehavior behavior))
            {
                return;
            }

            behavior.ScrollToOnMainThread(newValue, behavior.ScrollToPosition);
        }

        private void ScrollToOnMainThread(object targetElement, ScrollToPosition? scrollToPosition)
        {
            // Scrolling only works properly if the underlying ListView control
            // has been rendered; thus, we wait and reschedule ScrollTo when rendering has finished
            if (!this.isRendered)
            {
                return;
            }

            if (targetElement == null)
            {
                return;
            }

            if (!(scrollToPosition is ScrollToPosition scrollToPositionValue))
            {
                return;
            }

            // For some reasons, scrolling is not very accurate on iOS
            // if not executed on UI thread
            Device.BeginInvokeOnMainThread(() =>
            {
                try
                {
                    Tracer.Current.Debug($"ListViewScrollBehavior.ScrollToOnMainThread: targetElement={targetElement?.GetType().Name ?? "(null)"}, scrollToPosition={scrollToPositionValue}");
                    this.AssociatedObject.ScrollTo(targetElement, scrollToPositionValue, animated: true);
                }
                catch (Exception ex)
                {
                    Tracer.Current.Debug($"ListViewScrollBehavior.ScrollToOnMainThread failed with exception: {ex}");
                }
            });
        }
    }
}