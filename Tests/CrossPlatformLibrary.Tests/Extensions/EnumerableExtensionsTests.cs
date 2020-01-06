using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Tests.Stubs;
using FluentAssertions;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public partial class EnumerableExtensionsTests
    {
        [Fact]
        public void SortExtensionTest()
        {
            // Arrange.
            IList<int> expectedSortCollection = new Collection<int> { -1, 2, 3, 4, 5, 6, 10 };
            IList<int> sourceCollection = new Collection<int> { 5, 6, 10, 4, 2, 3, -1 };

            // Act.
            sourceCollection.Sort(i => i);

            // Assert.
            Assert.True(sourceCollection[0] == expectedSortCollection[0]);
            Assert.True(sourceCollection[1] == expectedSortCollection[1]);
            Assert.True(sourceCollection[2] == expectedSortCollection[2]);
            Assert.True(sourceCollection[3] == expectedSortCollection[3]);
            Assert.True(sourceCollection[4] == expectedSortCollection[4]);
            Assert.True(sourceCollection[5] == expectedSortCollection[5]);
            Assert.True(sourceCollection[6] == expectedSortCollection[6]);
        }

        [Fact]
        public void ForEachExtensionTest()
        {
            // Arrange.
            IList<int> expectedCollection = new Collection<int>();
            IList<int> sourceCollection = new Collection<int> { 5, 6, 10, 4, 2, 3, -1 };
            const int AddedOffset = 1;

            // Act.
            sourceCollection.ForEach(x => expectedCollection.Add(x + AddedOffset));

            // Assert.
            Assert.True(sourceCollection[0] == expectedCollection[0] - AddedOffset);
            Assert.True(sourceCollection[1] == expectedCollection[1] - AddedOffset);
            Assert.True(sourceCollection[2] == expectedCollection[2] - AddedOffset);
            Assert.True(sourceCollection[3] == expectedCollection[3] - AddedOffset);
            Assert.True(sourceCollection[4] == expectedCollection[4] - AddedOffset);
            Assert.True(sourceCollection[5] == expectedCollection[5] - AddedOffset);
            Assert.True(sourceCollection[6] == expectedCollection[6] - AddedOffset);
        }

        [Fact]
        public void ToObservableCollectionExtensionTest()
        {
            // Arrange.
            var expectedCollection = new ObservableCollection<int> { 5, 6, 10, 4, 2, 3, -1 };
            var sourceCollection = new List<int> { 5, 6, 10, 4, 2, 3, -1 };

            // Act.
            var resultCollection = sourceCollection.ToObservableCollection();

            // Assert.
            Assert.True(resultCollection[0] == expectedCollection[0]);
            Assert.True(resultCollection[1] == expectedCollection[1]);
            Assert.True(resultCollection[2] == expectedCollection[2]);
            Assert.True(resultCollection[3] == expectedCollection[3]);
            Assert.True(resultCollection[4] == expectedCollection[4]);
            Assert.True(resultCollection[5] == expectedCollection[5]);
            Assert.True(resultCollection[6] == expectedCollection[6]);
        }

        [Fact]
        public void ShouldAppendToList()
        {
            // Arrange.
            var sourceCollection = new List<int> { 1, 2, 3 };

            // Act.
            var resultCollection = sourceCollection.Append(99);

            // Assert
            resultCollection.Should().ContainInOrder(new List<int> { 1, 2, 3, 99 });
        }

        [Fact]
        public void ShouldPrependToList()
        {
            // Arrange.
            var sourceCollection = new List<int> { 1, 2, 3 };

            // Act.
            var resultCollection = sourceCollection.Prepend(99);

            // Assert
            resultCollection.Should().ContainInOrder(new List<int> { 99, 1, 2, 3 });
        }

        [Fact]
        public void ShouldFindDuplicates_AtLeastTwoDuplicates()
        {
            // Arrange.
            var sourceCollection = new List<Person>
            {
                new Person { Name = "A" },
                new Person { Name = "B" },
                new Person { Name = "B" },
                new Person { Name = "C" },
            };

            // Act
            var duplicates = sourceCollection.FindDuplicates(p => p.Name);

            // Assert
            duplicates.Should().ContainInOrder(new List<Person>
            {
                new Person { Name = "B" },
                new Person { Name = "B" }
            });
        }

        [Fact]
        public void ShouldFindDuplicates_AtLeastThreeDuplicates()
        {
            // Arrange.
            var sourceCollection = new List<Person>
            {
                new Person { Name = "A" },
                new Person { Name = "B" },
                new Person { Name = "B" },
                new Person { Name = "C" },
            };

            // Act
            var duplicates = sourceCollection.FindDuplicates(p => p.Name, numberOfDuplicates: 3);

            // Assert
            duplicates.Should().BeEmpty();
        }
    }
}