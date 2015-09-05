using CrossPlatformLibrary.Extensions;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Utils
{
    public class IntegerExtensionsTests
    {
        [Fact]
        public void IntegerIsOddTest()
        {
            // Arrange.
            const int Value = 1;

            // Act.
            bool isOdd = Value.IsOdd();

            // Assert.
            isOdd.Should().BeTrue();
        }

        [Fact]
        public void IntegerIsEvenTest()
        {
            // Arrange.
            const int Value = 2;

            // Act.
            bool isOdd = Value.IsOdd();

            // Assert.
            isOdd.Should().BeFalse();
        }
    }
}