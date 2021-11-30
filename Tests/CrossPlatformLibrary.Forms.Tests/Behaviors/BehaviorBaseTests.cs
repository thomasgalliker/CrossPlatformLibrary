using CrossPlatformLibrary.Forms.Tests.Testdata;
using FluentAssertions;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Behaviors
{
    public partial class BehaviorBaseTests
    {
        public BehaviorBaseTests()
        {
            MockForms.Init();
        }

        [Fact]
        public void ShouldSetAssociatedObject()
        {
            // Arrange
            var behavior = new TestBehavior();
            var targetControl = new ListView { BindingContext = new TestListViewModel() };

            // Act
            targetControl.Behaviors.Add(behavior);

            // Assert
            behavior.AssociatedObject.Should().Be(targetControl);
            behavior.OnAttachedToTimes.Should().Be(1);
            behavior.OnDetachingFromTimes.Should().Be(0);
        }

        [Fact]
        public void ShouldSetBindingContext()
        {
            // Arrange
            var behavior = new TestBehavior();
            var targetControl = new ListView { BindingContext = null };
            targetControl.Behaviors.Add(behavior);

            // Act
            targetControl.BindingContext = new TestListViewModel();

            // Assert
            behavior.BindingContext.Should().Be(targetControl.BindingContext);
            behavior.OnAttachedToTimes.Should().Be(1);
            behavior.OnDetachingFromTimes.Should().Be(0);
        }

        [Fact]
        public void ShouldSetBindingContextNull()
        {
            // Arrange
            var behavior = new TestBehavior();
            var targetControl = new ListView { BindingContext = new TestListViewModel() };
            targetControl.Behaviors.Add(behavior);

            // Act
            targetControl.BindingContext = null;

            // Assert
            behavior.BindingContext.Should().BeNull();
            behavior.OnAttachedToTimes.Should().Be(1);
            behavior.OnDetachingFromTimes.Should().Be(1);
        }
    }
}
