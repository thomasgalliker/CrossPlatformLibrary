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
            const int i1 = 1;
            const int i2 = 2;

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
            var person1 = new Person { Name = "Lorem" };
            var person2 = new Person { Name = "Ipsum" };
            var person3 = new Person { Name = "Dolor" };

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
            var person1 = new Person { Name = "Lorem" };
            var person2 = new Person { Name = "Ipsum" };
            var person3 = new Person { Name = "Dolor" };
            var person4 = new Person { Name = "Hulu" };

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(person1, person2, person3, person4);
            var hashCode2 = HashCodeHelper.GetHashCode(person1, person2, person3, person4);

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void ShouldGetHashCodeOfArray_SameObjects()
        {
            // Arrange
            var personLorem = new Person { Name = "Lorem" };
            var personIpsum = new Person { Name = "Ipsum" };
            var personDolor1 = new Person { Name = "Dolor" };
            var personDolor2 = new Person { Name = "Dolor" };

            var listOfPersons1 = new List<Person> { personLorem, personIpsum, personDolor1 }.ToArray();
            var listOfPersons2 = new List<Person> { personLorem, personIpsum, personDolor2 }.ToArray();

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(listOfPersons1);
            var hashCode2 = HashCodeHelper.GetHashCode(listOfPersons2);

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [Fact]
        public void ShouldGetHashCodeOfArray_DifferentObjects()
        {
            // Arrange
            var personLorem = new Person { Name = "Lorem" };
            var personIpsum = new Person { Name = "Ipsum" };
            var personDolor1 = new Person { Name = "Dolor" };
            var personDolor2 = new Person { Name = "Dolor3" };

            var listOfPersons1 = new List<Person> { personLorem, personIpsum, personDolor1 }.ToArray();
            var listOfPersons2 = new List<Person> { personLorem, personIpsum, personDolor2 }.ToArray();

            // Act
            var hashCode1 = HashCodeHelper.GetHashCode(listOfPersons1);
            var hashCode2 = HashCodeHelper.GetHashCode(listOfPersons2);

            // Assert
            hashCode1.Should().NotBe(hashCode2);
        }
    }
}