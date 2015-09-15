using System.Collections.Generic;
using System.Linq;

using CrossPlatformLibrary.IO;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.IO
{
    public class XmlSerializerHelperTests
    {
        [Fact]
        public void ShouldSerializeEmptyObject()
        {
            // Arrange
            object obj = new object();

            // Act
            var serializedString = obj.SerializeToXml();
            var deserializedObject = serializedString.DeserializeFromXml<object>();

            // Assert
            serializedString.Should().NotBeNullOrEmpty();
            deserializedObject.Should().NotBeNull();
        }

        [Fact]
        public void ShouldSerializeSimpleObject()
        {
            // Arrange
            object obj = new SimpleSerializerClass { BoolProperty = true, StringProperty = "test" };

            // Act
            var serializedString = obj.SerializeToXml();
            var deserializedObject = serializedString.DeserializeFromXml<SimpleSerializerClass>();

            // Assert
            serializedString.Should().NotBeNullOrEmpty();
            deserializedObject.Should().NotBeNull();
            deserializedObject.BoolProperty.Should().BeTrue();
            deserializedObject.StringProperty.Should().Be("test");
        }

        [Fact]
        public void ShouldSerializeConcreteList()
        {
            // Arrange
            List<string> list = new List<string> { "a", "b", "c" };

            // Act
            var serializedString = list.SerializeToXml();
            var deserializedList = serializedString.DeserializeFromXml<List<string>>();

            // Assert
            serializedString.Should().NotBeNullOrEmpty();
            deserializedList.Should().NotBeNullOrEmpty();
            deserializedList.Should().HaveCount(list.Count);
            deserializedList.ElementAt(0).Should().Be(list[0]);
            deserializedList.ElementAt(1).Should().Be(list[1]);
            deserializedList.ElementAt(2).Should().Be(list[2]);
        }

        [Fact]
        public void ShouldSerializeInterfaceList()
        {
            // Arrange
            IList<string> list = new List<string> { "a", "b", "c" };

            // Act
            var serializedString = list.SerializeToXml();
            var deserializedList = serializedString.DeserializeFromXml<IList<string>>();

            // Assert
            serializedString.Should().NotBeNullOrEmpty();
            deserializedList.Should().NotBeNullOrEmpty();
            deserializedList.Should().HaveCount(list.Count);
            deserializedList.ElementAt(0).Should().Be(list[0]);
            deserializedList.ElementAt(1).Should().Be(list[1]);
            deserializedList.ElementAt(2).Should().Be(list[2]);
        }

        public class SimpleSerializerClass
        {
            public string StringProperty { get; set; }

            public bool BoolProperty { get; set; }
        }
    }
}