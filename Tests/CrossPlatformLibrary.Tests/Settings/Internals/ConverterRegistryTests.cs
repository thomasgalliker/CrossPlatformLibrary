using CrossPlatformLibrary.Settings.Converters;
using CrossPlatformLibrary.Settings.Internals;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Settings.Internals
{
    public class ConverterRegistryTests
    {
        [Fact]
        public void ShouldTryConvert_SourceTypeSameAsTargetType1()
        {
            // Arrange
            var converterRegistry = new ConverterRegistry();
            var inputValue = (string)null;

            // Act
            var converted = converterRegistry.TryConvert(inputValue, typeof(string), typeof(string));

            // Assert
            converted.Should().Be(inputValue);
        }

        [Fact]
        public void ShouldTryConvert_SourceTypeSameAsTargetType2()
        {
            // Arrange
            var converterRegistry = new ConverterRegistry();
            var inputValue = "input";

            // Act
            var converted = converterRegistry.TryConvert(inputValue, typeof(double), typeof(string));

            // Assert
            converted.Should().Be(inputValue);
        }

        [Theory]
        [InlineData("1", 1d)]
        [InlineData("123.45", 123.45d)]
        public void ShouldTryConvert_UsingConverter(string input, double expectedOutput)
        {
            // Arrange
            var converterRegistry = new ConverterRegistry();
            converterRegistry.RegisterConverter<string, double>(new StringToDoubleConverter(), reverse: true);

            // Act
            var converted = converterRegistry.TryConvert(input, typeof(string), typeof(double));

            // Assert
            converted.Should().Be(expectedOutput);
        }
    }
}
