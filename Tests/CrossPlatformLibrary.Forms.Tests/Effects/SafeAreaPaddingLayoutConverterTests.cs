using CrossPlatformLibrary.Forms.Effects;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Effects
{
    public class SafeAreaPaddingLayoutConverterTests
    {
        [Theory]
        [ClassData(typeof(SafeAreaPaddingLayoutConverterValidTestdata))]
        public void ShouldConvertFromInvariantString_Valid(string inputString, SafeAreaPaddingLayout safeAreaPaddingLayout)
        {
            // Arrange
            var safeAreaPaddingLayoutConverter = new SafeAreaPaddingLayoutConverter();

            // Act
            var output = safeAreaPaddingLayoutConverter.ConvertFromInvariantString(inputString);

            // Assert
            output.Should().NotBeNull();
            output.Should().BeEquivalentTo(safeAreaPaddingLayout);
        }

        public class SafeAreaPaddingLayoutConverterValidTestdata : TheoryData<string, SafeAreaPaddingLayout>
        {
            public SafeAreaPaddingLayoutConverterValidTestdata()
            {
                this.Add("Left", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Left));
                this.Add("Top", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Top));
                this.Add("Right", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Right));
                this.Add("Bottom", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Bottom));

                this.Add("Top,Bottom", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Top, SafeAreaPaddingLayout.PaddingPosition.Bottom));
                this.Add("Top, Bottom", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Top, SafeAreaPaddingLayout.PaddingPosition.Bottom));
                this.Add("Left,Right,Top,Bottom", new SafeAreaPaddingLayout(SafeAreaPaddingLayout.PaddingPosition.Left, SafeAreaPaddingLayout.PaddingPosition.Right, SafeAreaPaddingLayout.PaddingPosition.Top, SafeAreaPaddingLayout.PaddingPosition.Bottom));
            }
        }
    }
}
