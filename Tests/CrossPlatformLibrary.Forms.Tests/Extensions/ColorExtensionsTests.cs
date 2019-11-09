using System.Diagnostics;
using CrossPlatformLibrary.Forms.Extensions;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Extensions
{
    public class ColorExtensionsTests
    {
        [Fact]
        public void ShouldComplement()
        {
            // Arrange
            var color = Color.FromHex("#FF0000");

            // Act
            var invertedColor = color.Complement();

            // Assert
            invertedColor.A.Should().Be(1);
            invertedColor.R.Should().Be(0);
            invertedColor.G.Should().Be(0.99164342880249023);
            invertedColor.B.Should().Be(1);
            invertedColor.Saturation.Should().Be(1.0);
            invertedColor.Luminosity.Should().Be(0.5);
        }

        [Fact]
        public void ShouldInvert()
        {
            // Arrange
            var color = Color.FromHex("#FF0000");

            // Act
            var invertedColor = color.Invert();

            // Assert
            invertedColor.Should().Be(Color.FromHex("#00FFFF"));
        }

        [Fact]
        public void ShouldInvert_WithAlpha()
        {
            // Arrange
            var color = Color.FromHex("#55FF0000");

            // Act
            var invertedColor = color.Invert();

            // Assert
            invertedColor.Should().Be(Color.FromHex("#5500FFFF"));
        }
    }
}
