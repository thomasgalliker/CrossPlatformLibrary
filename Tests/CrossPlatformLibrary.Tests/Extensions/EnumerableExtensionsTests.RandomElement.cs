using System;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public partial class EnumerableExtensionsTests
    {
        [Fact]
        public void NullSourceTest()
        {
            // Arrange
            int[] values = null;

            // Act
            Action action = () => values.RandomElement();

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void EmptySourceTest()
        {
            // Arrange
            var values = Enumerable.Empty<int>();

            // Act
            Action action = () => values.RandomElement();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void IEnumerableTest()
        {
            // Arrange
            var values = Enumerable.Range(0, 1000);

            // Act
            var result1 = values.RandomElement(new Random(1));
            var result2 = values.RandomElement(new Random(2));

            // Assert
            Assert.NotEqual(result1, result2);
        }

        [Fact]
        public void IListTest()
        {
            // Arrange
            var values = Enumerable.Range(0, 1000).ToList();

            // Act
            var result1 = values.RandomElement(new Random(1));
            var result2 = values.RandomElement(new Random(2));

            // Assert
            Assert.NotEqual(result1, result2);
        }

        [Fact]
        public void SingleElementTest()
        {
            var values1 = Enumerable.Range(10, 1);
            int result1 = values1.RandomElement(new Random(1));
            Assert.Equal(10, result1);

            var values2 = values1.ToList();
            int result2 = values2.RandomElement(new Random(2));
            Assert.Equal(10, result2);
        }

        [Fact]
        public void BoundaryTest()
        {
            // Arrange
            var values = Enumerable.Range(10, 2);
            var result1 = values.RandomElement();
            var foundDifferent = false;
            for (var i = 0; i < 25; i++)
            {
                var result2 = values.RandomElement(new Random(i + 100));
                if (result1 != result2)
                {
                    foundDifferent = true;
                    break;
                }
            }

            // Assert
            Assert.True(foundDifferent);
        }

        //[Fact]
        //public void CheckForDefault()
        //{
        //    var values = Enumerable.Empty<int>();
        //    int result = values.RandomElementOrDefault();

        //    Assert.Equal(0, result);
        //}
    }
}