using System.Collections.Generic;

using CrossPlatformLibrary.Tests.Stubs;
using CrossPlatformLibrary.Utils;


using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.IO
{
    public class HashCodeHelperTests
    {
        [Fact]
        public void ShouldGetHashCodeOf2Values()
        {
            // Arrange
            int i1 = 1;
            int i2 = 2;

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(i1, i2);
            var hashCode2 = HashCodeHelper.GetHashCode(i1, i2);

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void ShouldGetHashCodeOf3Values()
        {
            // Arrange
            Person person1 = new Person{ Name = "Lorem"};
            Person person2 = new Person{ Name = "Ipsum"};
            Person person3 = new Person{ Name = "Dolor"};

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(person1, person2, person3);
            var hashCode2 = HashCodeHelper.GetHashCode(person1, person2, person3);

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void ShouldGetHashCodeOf4Values()
        {
            // Arrange
            Person person1 = new Person { Name = "Lorem" };
            Person person2 = new Person { Name = "Ipsum" };
            Person person3 = new Person { Name = "Dolor" };
            Person person4 = new Person { Name = "Hulu" };

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(person1, person2, person3, person4);
            var hashCode2 = HashCodeHelper.GetHashCode(person1, person2, person3, person4);

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void ShouldGetHashCodeOfArray()
        {
            // Arrange
            Person person1 = new Person { Name = "Lorem" };
            Person person2 = new Person { Name = "Ipsum" };
            Person person3 = new Person { Name = "Dolor" };
            Person person4 = new Person { Name = "Dolor" };
            Person[] listOfPersons = new List<Person> { person1, person2, person3 }.ToArray();
            Person[] listOfPersons2 = new List<Person> { person1, person2, person4 }.ToArray();

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(listOfPersons);
            var hashCode2 = HashCodeHelper.GetHashCode(listOfPersons);
            var hashCode3 = HashCodeHelper.GetHashCode(listOfPersons2);

            // Assert
            hashCode1.Should().Be(hashCode2);
            hashCode2.Should().NotBe(hashCode3);
        }
    }
}
