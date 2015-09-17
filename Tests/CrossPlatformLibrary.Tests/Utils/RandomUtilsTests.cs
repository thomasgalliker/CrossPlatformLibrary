
using CrossPlatformLibrary.Utils;


using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Utils
{
    public class RandomUtilsTests
    {
        [Fact]
        public void ShouldGenerateRandomStringWithCorrectLength()
        {
            // Arrange
            int length = 200;

            // Act
            var randomString = RandomUtils.GenerateRandomString(length, RandomUtils.AlphaNumeric.AlphaNumeric);

            // Assert
            randomString.Should().NotBeEmpty();
            randomString.Should().HaveLength(length);
        }

        [Fact]
        public void ShouldGenerateRandomAlphaString()
        {
            // Arrange
            int length = 1000;

            // Act
            var randomString = RandomUtils.GenerateRandomString(length, RandomUtils.AlphaNumeric.AlphaOnly);

            // Assert
            randomString.Should().NotBeEmpty();
            randomString.Should().Contain("A");
            randomString.Should().NotContain("0");
        }

        [Fact]
        public void ShouldGenerateRandomNumericString()
        {
            // Arrange
            int length = 1000;

            // Act
            var randomString = RandomUtils.GenerateRandomString(length, RandomUtils.AlphaNumeric.NumericOnly);

            // Assert
            randomString.Should().NotBeEmpty();
            randomString.Should().Contain("0");
            randomString.Should().NotContain("A");
        }
    }
}