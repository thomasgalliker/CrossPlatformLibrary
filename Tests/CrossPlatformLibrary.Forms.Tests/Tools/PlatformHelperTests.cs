using System;
using System.Linq;
using CrossPlatformLibrary.Forms.Tools;
using FluentAssertions;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Tools
{
    public class PlatformHelperTests
    {
        public PlatformHelperTests()
        {
            MockForms.Init();
        }

        [Fact]
        public void ShouldRunOnPlatform_NoActions()
        {
            // Act
            Action action = () => PlatformHelper.RunOnPlatform();

            // Assert
            action.Should().NotThrow();
        }

        [Fact]
        public void ShouldRunOnPlatform_OneAction()
        {
            // Arrange
            var currentRuntimePlatform = Device.RuntimePlatform;
            var currentRuntimePlatformCalls = 0;
            var iosCalls = 0;
            var androidCalls = 0;

            // Act
            PlatformHelper.RunOnPlatform(
                (Device.iOS, () => { iosCalls++; }
                ),
                (Device.Android, () => { androidCalls++; }
                ),
                (currentRuntimePlatform, () => { currentRuntimePlatformCalls++; }
                ));

            // Assert
            currentRuntimePlatformCalls.Should().Be(1);
            iosCalls.Should().Be(0);
            androidCalls.Should().Be(0);
        }

        [Fact]
        public void ShouldRunOnPlatform_MultipleActions()
        {
            // Arrange
            var currentRuntimePlatform = Device.RuntimePlatform;
            var currentRuntimePlatformCalls = 0;
            var iosCalls = 0;
            var androidCalls = 0;

            // Act
            PlatformHelper.RunOnPlatform(
                (Device.iOS, () => { iosCalls++; }
                ),
                (Device.Android, () => { androidCalls++; }
                ),
                (currentRuntimePlatform, () => { currentRuntimePlatformCalls++; }
                ),
                (currentRuntimePlatform, () => { currentRuntimePlatformCalls++; }
                ),
                (currentRuntimePlatform, () => { currentRuntimePlatformCalls++; }
                ));

            // Assert
            currentRuntimePlatformCalls.Should().Be(3);
            iosCalls.Should().Be(0);
            androidCalls.Should().Be(0);
        }

        [Fact]
        public void OnPlatformValue_ShouldReturnPlatformValue()
        {
            // Arrange
            var currentRuntimePlatform = Device.RuntimePlatform;

            // Act
            var platformValues = PlatformHelper.OnPlatformValue(
                (Device.iOS, () => 1
                ),
                (Device.Android, () => 2
                ),
                (currentRuntimePlatform, () => 3
                ));

            // Assert
            platformValues.Should().Be(3);
        }

        [Fact]
        public void OnPlatformValues_ShouldReturnPlatformValues()
        {
            // Arrange
            var currentRuntimePlatform = Device.RuntimePlatform;

            // Act
            var platformValues = PlatformHelper.OnPlatformValues(
                (Device.iOS, () => 1
                ),
                (Device.Android, () => 2
                ),
                (currentRuntimePlatform, () => 3
                ),
                (currentRuntimePlatform, () => 4
                ));

            // Assert
            platformValues.Should().HaveCount(2);
            platformValues.ElementAt(0).Should().Be(3);
            platformValues.ElementAt(1).Should().Be(4);
        }
    }
}