using System;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class ScrollViewBehavior : BehaviorBase<ScrollView>
    {
        private Point offset;

        protected override void OnAttachedTo(ScrollView bindable)
        {
            base.OnAttachedTo(bindable);

            bindable.Scrolled += this.OnScrollViewScrolled;
            bindable.SizeChanged += this.OnScrollViewSizeChanged;
        }

        protected override void OnDetachingFrom(ScrollView bindable)
        {
            bindable.Scrolled -= this.OnScrollViewScrolled;
            bindable.SizeChanged -= this.OnScrollViewSizeChanged;
            base.OnDetachingFrom(bindable);
        }

        private void OnScrollViewSizeChanged(object sender, EventArgs e)
        {
            var scrollView = (ScrollView)sender;

            this.offset = GetOffset(scrollView, this.Element);

            this.UpdateIsInScollArea(scrollView, this.Element);
        }

        private static Point GetOffset(ScrollView scrollView, VisualElement visualElement)
        {
            var result = new Point(visualElement.X, visualElement.Y);

            var parent = visualElement.Parent as VisualElement;
            while (parent != scrollView && parent != null)
            {
                result = result.Offset(parent.X, parent.Y);

                parent = parent.Parent as VisualElement;
            }

            return result;
        }

        private void OnScrollViewScrolled(object sender, ScrolledEventArgs e)
        {
            if (!(sender is ScrollView scrollView))
            {
                return;
            }

            this.UpdateIsInScollArea(scrollView, this.Element);
        }

        public static readonly BindableProperty ElementProperty =
            BindableProperty.Create(
               nameof(Element),
               typeof(VisualElement),
               typeof(ScrollViewBehavior),
               null,
               propertyChanged: OnElementPropertyChanged);

        public VisualElement Element
        {
            get => (VisualElement)this.GetValue(ElementProperty);
            set => this.SetValue(ElementProperty, value);
        }

        private static void OnElementPropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is ScrollViewBehavior behavior))
            {
                return;
            }

            if (!(behavior.AssociatedObject is ScrollView scrollView))
            {
                return;
            }

            if (!(newValue is VisualElement element))
            {
                return;
            }

            behavior.UpdateIsInScollArea(scrollView, element);
        }

        private void UpdateIsInScollArea(ScrollView scrollView, VisualElement element)
        {
            bool isInScollArea;

            if (scrollView.ScrollY > element.Height + this.offset.Y)
            {
                isInScollArea = false;
            }
            else
            {
                isInScollArea = true;
            }

            Tracer.Current.Debug($"UpdateIsInScollArea: scrollView.ScaleY={scrollView.ScaleY}, element.Height={element.Height}, offset.Y={this.offset.Y} -> isInScollArea={isInScollArea}");
            this.IsInScollArea = isInScollArea;
        }

        public static readonly BindableProperty IsInScollAreaProperty =
            BindableProperty.Create(
                nameof(IsInScollArea),
                typeof(bool),
                typeof(ScrollViewBehavior),
                false,
                BindingMode.OneWayToSource);


        public bool IsInScollArea
        {
            get => (bool)this.GetValue(IsInScollAreaProperty);
            set => this.SetValue(IsInScollAreaProperty, value);
        }
    }
}