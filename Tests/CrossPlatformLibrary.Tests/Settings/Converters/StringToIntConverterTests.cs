using System;
using CrossPlatformLibrary.Settings.Converters;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Settings.Converters
{
    public class StringToIntConverterTests
    {
        [Theory]
        [InlineData(0, "0")]
        [InlineData(123, "123")]
        [InlineData(-123, "-123")]
        public void ShouldConvert_IntToString(int inputValue, string expectedOutputValue)
        {
            // Arrange
            var converter = new StringToIntConverter();

            // Act
            var convertedValue = converter.Convert(inputValue, typeof(int), typeof(string));

            // Assert
            convertedValue.Should().Be(expectedOutputValue);
        }

        [Theory]
        [InlineData("0", 0)]
        [InlineData("123", 123)]
        [InlineData("-123", -123)]
        public void ShouldConvert_StringToInt(string inputValue, int expectedOutputValue)
        {
            // Arrange
            var converter = new StringToIntConverter();

            // Act
            var convertedValue = converter.Convert(inputValue, typeof(string), typeof(int));

            // Assert
            convertedValue.Should().Be(expectedOutputValue);
        }


        [Fact]
        public void ShouldConvert_ThrowsExceptionIfCannotConvert()
        {
            // Arrange
            var converter = new StringToIntConverter();
            var inputValue = (int?)null;

            // Act
            Action action = () => converter.Convert(inputValue, typeof(int), typeof(string));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}
