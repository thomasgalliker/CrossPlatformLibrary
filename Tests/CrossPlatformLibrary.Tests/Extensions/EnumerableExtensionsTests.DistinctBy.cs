using System.Collections.Generic;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public partial class EnumerableExtensionsTests
    {
        [Fact]
        public void ShouldReturnDistinctBy_PropertyString()
        {
            // Arrange
            var source = new List<Person>
            {
                new Person { Name = "A" },
                new Person { Name = "B" },
                new Person { Name = "B" },
                new Person { Name = "C" },
            };

            // Act
            var distinct = source.DistinctBy(p => p.Name);

            // Assert
            distinct.Should().ContainInOrder(new List<Person>
            {
                new Person { Name = "A" },
                new Person { Name = "B" },
                new Person { Name = "C" },
            });
        }

        [Fact]
        public void ShouldReturnDistinctBy_PropertyInteger()
        {
            // Arrange
            var source = new[] { "first", "second", "third", "fourth", "fifth" };

            // Act
            var distinct = source.DistinctBy(word => word.Length);

            // Assert
            distinct.Should().ContainInOrder("first", "second");
        }
    }
}