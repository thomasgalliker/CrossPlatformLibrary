using CrossPlatformLibrary.Forms.Behaviors;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Tests.Testdata
{
    internal class TestBehavior : BehaviorBase<ListView>
    {
        public TestBehavior()
        {
        }

        public int OnAttachedToTimes { get; private set; }

        public int OnDetachingFromTimes { get; private set; }

        protected override void OnAttachedTo(ListView bindable)
        {
            this.OnAttachedToTimes++;
            base.OnAttachedTo(bindable);
        }

        protected override void OnDetachingFrom(ListView bindable)
        {
            this.OnDetachingFromTimes++;
            base.OnDetachingFrom(bindable);
        }
    }
}
