using System;
using System.Collections.Generic;
using System.Text;
using CrossPlatformLibrary.Forms.Effects;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Effects
{
    public class SafeAreaPaddingLayoutTests
    {
        [Fact]
        public void ShouldTransformLayoutPositionsToPadding_NoPositions()
        {
            // Arrange
            var safeAreaPaddingLayout = new SafeAreaPaddingLayout();
            var paddingInput = new Thickness(10);

            // Act
            var paddingOutput = safeAreaPaddingLayout.Transform(paddingInput);

            // Assert
            paddingOutput.Should().BeEquivalentTo(new Thickness(10, 10, 10, 10));
        }

        [Fact]
        public void ShouldTransformLayoutPositionsToPadding_TopAndBottom()
        {
            // Arrange
            var safeAreaPaddingLayout = new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Top, SafeAreaPaddingLayout.PaddingPosition.Bottom);
            var paddingInput = new Thickness(10, 10, 10, 10);

            // Act
            var paddingOutput = safeAreaPaddingLayout.Transform(paddingInput);

            // Assert
            paddingOutput.Should().BeEquivalentTo(new Thickness(0, 10, 0, 10));
        }
    }
}
