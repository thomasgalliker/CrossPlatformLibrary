using System.Threading.Tasks;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class VisualElementBehavior : BehaviorBase<VisualElement>
    {
        private const double CollapsedHeight = 0d;
        private double? originalHeight;
        private bool isAnimating;

        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create(
                nameof(IsVisible),
                typeof(bool),
                typeof(VisualElementBehavior),
                defaultValue: true,
                propertyChanged: OnIsVisiblePropertyChanged);

        public bool IsVisible
        {
            get => (bool)this.GetValue(IsVisibleProperty);
            set => this.SetValue(IsVisibleProperty, value);
        }

        public static readonly BindableProperty AnimationStepsProperty =
            BindableProperty.Create(
                nameof(AnimationSteps),
                typeof(uint),
                typeof(VisualElementBehavior),
                (uint)16);

        public uint AnimationSteps
        {
            get => (uint)this.GetValue(AnimationStepsProperty);
            set => this.SetValue(AnimationStepsProperty, value);
        }

        public static readonly BindableProperty AnimationLengthProperty =
            BindableProperty.Create(
                nameof(AnimationLength),
                typeof(uint),
                typeof(VisualElementBehavior),
                (uint)250);

        public uint AnimationLength
        {
            get => (uint)this.GetValue(AnimationLengthProperty);
            set => this.SetValue(AnimationLengthProperty, value);
        }

        public static readonly BindableProperty AnimationEasingProperty =
            BindableProperty.Create(
                nameof(AnimationEasing),
                typeof(Easing),
                typeof(VisualElementBehavior),
                Easing.CubicInOut);

        public Easing AnimationEasing
        {
            get => (Easing)this.GetValue(AnimationEasingProperty);
            set => this.SetValue(AnimationEasingProperty, value);
        }

        private static async void OnIsVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is VisualElementBehavior behavior))
            {
                return;
            }

            if (newValue is bool isVisible)
            {
                if (isVisible)
                {
                    await behavior.ExpandAsync(behavior.AnimationSteps, behavior.AnimationLength, behavior.AnimationEasing);
                }
                else
                {
                    await behavior.CollapseAsync(behavior.AnimationSteps, behavior.AnimationLength, behavior.AnimationEasing);
                }
            }
        }

        private async Task CollapseAsync(uint animationSteps, uint animationLength, Easing animationEasing)
        {
            if (this.isAnimating)
            {
                return;
            }

            this.isAnimating = true;

            if (this.originalHeight == null)
            {
                this.originalHeight = this.AssociatedObject.Height;
            }

            Tracer.Current.Debug($"CollapseAsync: {this.originalHeight} -> 0");

            if (animationSteps > 0 && animationLength > 0)
            {
                var animation = new Animation(v => this.AssociatedObject.HeightRequest = v, this.originalHeight.Value, CollapsedHeight);
                await this.AssociatedObject.AnimateAsync("ResizeHeightAnimation", animation, animationSteps, animationLength, animationEasing);
            }
            else
            {
                this.AssociatedObject.HeightRequest = CollapsedHeight;
            }

            this.AssociatedObject.IsVisible = false;

            this.isAnimating = false;
        }

        private async Task ExpandAsync(uint animationLength, uint animationSteps, Easing animationEasing)
        {
            if (this.isAnimating)
            {
                return;
            }

            this.isAnimating = true;

            Tracer.Current.Debug($"ExpandAsync: 0 -> {this.originalHeight}");
            this.AssociatedObject.IsVisible = true;

            if (animationSteps > 0 && animationLength > 0)
            {
                var animation = new Animation(v => this.AssociatedObject.HeightRequest = v, CollapsedHeight, this.originalHeight.Value);
                await this.AssociatedObject.AnimateAsync("ResizeHeightAnimation", animation, animationLength, animationSteps, animationEasing);
            }
            else
            {
                this.AssociatedObject.HeightRequest = this.originalHeight.Value;
            }

            this.isAnimating = false;
        }
    }

    public static class ViewExtensions
    {
        public static Task<bool> AnimateAsync(this VisualElement element, string name, Animation animation, uint steps, uint length, Easing easing)
        {
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate(name, animation, steps, length, easing, (v, c) => taskCompletionSource.SetResult(c));
            return taskCompletionSource.Task;
        }
    }
}