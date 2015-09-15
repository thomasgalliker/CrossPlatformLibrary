
using System;

using CrossPlatformLibrary.Extensions;
using CrossPlatformLibrary.Tests.Stubs;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class TypeExtensionsTests
    {
        #region GetDefaultValue Tests
        [Fact]
        public void ShouldGetDefaultValueForValueType()
        {
            // Arrange
            int i = 1;

            // Act
            var defaultValue = i.GetType().GetDefaultValue();

            // Assert
            defaultValue.Should().Be(default(int));
        }

        [Fact]
        public void ShouldGetDefaultValueForReferenceType()
        {
            // Arrange
            Person person = new Person();

            // Act
            var defaultValue = person.GetType().GetDefaultValue();

            // Assert
            defaultValue.Should().Be(default(Person));
        }
        #endregion

        #region IsNullable Tests
        [Fact]
        public void ShouldBeNullableType()
        {
            // Arrange
            var nullableType = typeof(bool?);

            // Act
            var isNullable = nullableType.IsNullable();

            // Assert
            isNullable.Should().BeTrue();
        }

        [Fact]
        public void ShouldNotBeNullableType()
        {
            // Arrange
            var valueType = typeof(bool);

            // Act
            var isNullable = valueType.IsNullable();

            // Assert
            isNullable.Should().BeFalse();
        }
        #endregion
        #region ToTypeName Tests
        [Fact]
        public void ShouldGetTypeNameOfValueType()
        {
            // Arrange
            Type type = typeof(String);

            // Act
            var typeName = type.ToTypeName();

            // Assert
            typeName.Should().Be("String");
        }

        [Fact]
        public void ShouldGetTypeNameOfReferenceType()
        {
            // Arrange
            Type type = typeof(Person);

            // Act
            var typeName = type.ToTypeName();

            // Assert
            typeName.Should().Be("Person");
        }

        [Fact]
        public void ShouldGetTypeNameOfGenericType()
        {
            // Arrange
            Type type = typeof(GenericClass1<string, int>);

            // Act
            var typeName = type.ToTypeName();

            // Assert
            typeName.Should().Be("GenericClass1<String, Int32>");
        }

        [Fact]
        public void ShouldGetTypeNameOfNestedGenericType()
        {
            // Arrange
            Type type = typeof(GenericClass1<GenericClass1<int, float>, Person>);

            // Act
            var typeName = type.ToTypeName();

            // Assert
            typeName.Should().Be("GenericClass1<GenericClass1<Int32, Single>, Person>");
        }

        [Fact]
        public void ShouldGetTypeNameOfNullableType()
        {
            // Arrange
            Type type = typeof(bool?);

            // Act
            var typeName = type.ToTypeName();

            // Assert
            typeName.Should().Be("Nullable<Boolean>");
        }
        #endregion
    }
}