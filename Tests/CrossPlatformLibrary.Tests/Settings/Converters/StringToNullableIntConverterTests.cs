using CrossPlatformLibrary.Settings.Converters;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Settings.Converters
{
    public class StringToNullableIntConverterTests
    {
        [Theory]
        [InlineData(null, null)]
        [InlineData(0, "0")]
        [InlineData(123, "123")]
        [InlineData(-123, "-123")]
        public void ShouldConvert_NullableIntToString(int? inputValue, string expectedOutputValue)
        {
            // Arrange
            var converter = new StringToNullableIntConverter();

            // Act
            var convertedValue = converter.Convert(inputValue, typeof(int?), typeof(string));

            // Assert
            convertedValue.Should().Be(expectedOutputValue);
        }

        [Theory]
        [InlineData(null, null)]
        [InlineData("0", 0)]
        [InlineData("123", 123)]
        [InlineData("-123", -123)]
        public void ShouldConvert_StringToNullableInt(string inputValue, int? expectedOutputValue)
        {
            // Arrange
            var converter = new StringToNullableIntConverter();

            // Act
            var convertedValue = converter.Convert(inputValue, typeof(string), typeof(int?));

            // Assert
            convertedValue.Should().Be(expectedOutputValue);
        }
    }
}
