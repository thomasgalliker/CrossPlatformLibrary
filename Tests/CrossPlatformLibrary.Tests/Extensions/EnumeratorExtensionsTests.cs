using System.Collections;
using System.Collections.Generic;
using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Tests.Stubs;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class EnumeratorExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ToEnumerableTestData))]
        public void ShouldCastEnumeratorToEnumerable(IEnumerator input, IEnumerable<Person> expectedOutput)
        {
            // Act
            var output = input.ToEnumerable<Person>();

            // Assert
            output.Should().ContainInOrder(expectedOutput);
        }

        [Theory]
        [ClassData(typeof(ToEnumerableTestData))]
        public void ShouldCastEnumeratorToList(IEnumerator input, List<Person> expectedOutput)
        {
            // Act
            var output = input.ToList<Person>();

            // Assert
            output.Should().ContainInOrder(expectedOutput);
        }

        public class ToEnumerableTestData : TheoryData<IEnumerator, IEnumerable<Person>>
        {
            public ToEnumerableTestData()
            {
                this.Add(null, new List<Person>());
                this.Add(new List<Person> { new Person() }.GetEnumerator(), new List<Person> { new Person() });
            }
        }
    }
}