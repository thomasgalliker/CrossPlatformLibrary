using System.Threading.Tasks;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Behaviors
{
    public class VisualElementVisibilityAnimationBehavior : BehaviorBase<VisualElement>
    {
        private bool isAnimating;
        private double? originalHeight;

        public static readonly BindableProperty IsVisibleProperty =
            BindableProperty.Create(
                nameof(IsVisible),
                typeof(bool),
                typeof(VisualElementVisibilityAnimationBehavior),
                defaultValue: true,
                propertyChanged: OnIsVisiblePropertyChanged);

        public bool IsVisible
        {
            get => (bool)this.GetValue(IsVisibleProperty);
            set => this.SetValue(IsVisibleProperty, value);
        }

        private static async void OnIsVisiblePropertyChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (!(bindable is VisualElementVisibilityAnimationBehavior behavior))
            {
                return;
            }

            if (newValue is bool isVisible)
            {
                if (isVisible)
                {
                    await behavior.ExpandAsync();
                }
                else
                {
                    await behavior.CollapseAsync();
                }
            }
        }

        private async Task CollapseAsync()
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

            var animation = new Animation(v => this.AssociatedObject.HeightRequest = v, this.originalHeight.Value, 0d);
            await this.AssociatedObject.AnimateAsync("ResizeHeightAnimation", animation, easing: Easing.CubicInOut);

            this.AssociatedObject.IsVisible = false;

            this.isAnimating = false;
        }

        private async Task ExpandAsync()
        {
            if (this.isAnimating)
            {
                return;
            }

            this.isAnimating = true;

            Tracer.Current.Debug($"ExpandAsync: 0 -> {this.originalHeight}");
            this.AssociatedObject.IsVisible = true;

            var animation = new Animation(v => this.AssociatedObject.HeightRequest = v, 0d, this.originalHeight.Value);
            await this.AssociatedObject.AnimateAsync("ResizeHeightAnimation", animation, easing: Easing.CubicInOut);

            this.isAnimating = false;
        }
    }

    public static class ViewExtensions
    {
        public static Task<bool> AnimateAsync(this VisualElement element, string name, Animation animation, uint steps = 16u, uint length = 250u, Easing easing = null)
        {
            easing = easing ?? Easing.Linear;
            var taskCompletionSource = new TaskCompletionSource<bool>();

            element.Animate(name, animation, steps, length, easing, (v, c) => taskCompletionSource.SetResult(c));
            return taskCompletionSource.Task;
        }
    }
}