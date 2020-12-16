using System;
using System.Collections.Generic;
using System.Text;
using CrossPlatformLibrary.Forms.Tests.Testdata;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;
using FluentAssertions;

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
            var targetControl = new ListView { BindingContext = new object() };

            // Act
            targetControl.Behaviors.Add(behavior);

            // Assert
            behavior.AssociatedObject.Should().Be(targetControl);
        }

        [Fact]
        public void ShouldUpdateBindingContext()
        {
            // Arrange
            var behavior = new TestBehavior();
            var targetControl = new ListView { BindingContext = null };
            targetControl.Behaviors.Add(behavior);

            // Act
            targetControl.BindingContext = new object();

            // Assert
            behavior.BindingContext.Should().Be(targetControl.BindingContext);
        }
    }
}
