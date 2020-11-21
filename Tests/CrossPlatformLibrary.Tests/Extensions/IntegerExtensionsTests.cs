using CrossPlatformLibrary.Extensions;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class IntegerExtensionsTests
    {
        [Fact]
        public void IntegerIsOddTest()
        {
            // Arrange.
            const int value = 1;

            // Act.
            bool isOdd = value.IsOdd();

            // Assert.
            isOdd.Should().BeTrue();
        }

        [Fact]
        public void IntegerIsEvenTest()
        {
            // Arrange.
            const int value = 2;

            // Act.
            bool isOdd = value.IsOdd();

            // Assert.
            isOdd.Should().BeFalse();
        }
    }
}