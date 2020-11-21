using System.Collections.Generic;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public partial class EnumerableExtensionsTests
    {
        private readonly IEnumerable<int> testCollection = Enumerable.Range(1, 10000);

        [Fact]
        public void Shuffle_ShouldNotAlterTheSizeOfTheEnumerable()
        {
            this.testCollection
                .Shuffle()
                .Count()
                .Should().Be(this.testCollection.Count());
        }

        [Fact]
        public void Shuffle_ShouldContainAllElementsFromTheBaseEnumerable()
        {
            var shuffled = this.testCollection.Shuffle().ToArray();
            foreach (var item in this.testCollection)
            {
                shuffled.Should().Contain(item);
            }
        }

        [Fact]
        public void Shuffle_ShouldNotReturnElementInTheSameOrderAsBaseEnumerable()
        {
            this.testCollection.Shuffle()
                .SequenceEqual(this.testCollection)
                .Should().BeFalse();
        }

        [Fact]
        public void Shuffle_ShouldReturnDifferentOrderOnEachEnumeration()
        {
            var shuffled = this.testCollection.Shuffle();

            shuffled
                .ToArray()
                .SequenceEqual(shuffled.ToArray())
                .Should().BeFalse();
        }

        [Fact]
        public void Shuffle_ShouldAlmostNeverBeEqual()
        {
            for (var i = 0; i < 50; i++)
            {
                Assert.False(this.testCollection.SequenceEqual(this.testCollection.Shuffle()));
            }
        }
    }
}