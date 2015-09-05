using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using CrossPlatformLibrary.Extensions;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class EnumerableExtensionsTests
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
        public void ToConsecutiveGroupsExtensionTestWithEmptyList()
        {
            // Arrange.
            var sourceCollection = new List<int>();

            // Act.
            var resultListOfGroups = sourceCollection.ToConsecutiveGroups().ToList();

            // Assert.
            resultListOfGroups.Should().NotBeNull();
            resultListOfGroups.Should().HaveCount(0);
        }

        [Fact]
        public void ToConsecutiveGroupsExtensionTest()
        {
            // Arrange.
            var sourceCollection = new List<int> { 5, 6, 7, 10, -2, -1, 3, 0 };

            // Act.
            var resultListOfGroups = sourceCollection.ToConsecutiveGroups().ToList();

            // Assert.
            resultListOfGroups.Should().NotBeNull();
            resultListOfGroups.Should().HaveCount(5);
            Assert.True(resultListOfGroups[0].Count() == 3);
            Assert.True(resultListOfGroups[1].Count() == 1);
            Assert.True(resultListOfGroups[2].Count() == 2);
            Assert.True(resultListOfGroups[3].Count() == 1);
            Assert.True(resultListOfGroups[4].Count() == 1);
        }
    }
}