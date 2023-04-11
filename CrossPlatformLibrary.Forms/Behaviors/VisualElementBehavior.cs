using System.Threading.Tasks;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;
using CrossPlatformLibrary.Forms.Extensions;

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

            if (!(behavior.AssociatedObject is VisualElement associatedObject))
            {
                return;
            }

            if (newValue is bool isVisible)
            {
                if (isVisible)
                {
                    await behavior.ExpandAsync(associatedObject, behavior.AnimationSteps, behavior.AnimationLength, behavior.AnimationEasing);
                }
                else
                {
                    await behavior.CollapseAsync(associatedObject, behavior.AnimationSteps, behavior.AnimationLength, behavior.AnimationEasing);
                }
            }
        }

        private async Task CollapseAsync(VisualElement associatedObject, uint animationSteps, uint animationLength, Easing animationEasing)
        {
            if (this.isAnimating)
            {
                return;
            }

            this.isAnimating = true;

            if (this.originalHeight == null)
            {
                this.originalHeight = associatedObject.Height;
            }

            Tracer.Current.Debug($"CollapseAsync: {this.originalHeight} -> 0");

            if (animationSteps > 0 && animationLength > 0)
            {
                var animation = new Animation(v => associatedObject.HeightRequest = v, this.originalHeight.Value, CollapsedHeight);
                await associatedObject.AnimateAsync("ResizeHeightAnimation", animation, animationSteps, animationLength, animationEasing);
            }
            else
            {
                associatedObject.HeightRequest = CollapsedHeight;
            }

            associatedObject.IsVisible = false;

            this.isAnimating = false;
        }

        private async Task ExpandAsync(VisualElement associatedObject, uint animationLength, uint animationSteps, Easing animationEasing)
        {
            if (this.isAnimating)
            {
                return;
            }

            this.isAnimating = true;

            Tracer.Current.Debug($"ExpandAsync: 0 -> {this.originalHeight}");
            associatedObject.IsVisible = true;

            if (animationSteps > 0 && animationLength > 0)
            {
                var animation = new Animation(v => associatedObject.HeightRequest = v, CollapsedHeight, this.originalHeight.Value);
                await associatedObject.AnimateAsync("ResizeHeightAnimation", animation, animationLength, animationSteps, animationEasing);
            }
            else
            {
                associatedObject.HeightRequest = this.originalHeight.Value;
            }

            this.isAnimating = false;
        }
    }
}