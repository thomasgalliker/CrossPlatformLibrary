using System;
using System.Collections.Generic;

using CrossPlatformLibrary.IO;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.IO
{
    public class XmlSerializerHelperTests
    {
        [Fact]
        public void ShouldSerializeAnEmptyObject()
        {
            // Arrange
            object obj = new object();

            // Act
            var serializedString = obj.SerializeToXml();
            var deserializedObject = serializedString.DeserializeFromXml<object>();

            // Assert
            Assert.NotNull(serializedString);
            Assert.NotNull(deserializedObject);
        }

        [Fact]
        public void ShouldSerializeAList()
        {
            // Arrange
            List<string> list = new List<string> { "a", "b", "c" };

            // Act
            var serializedString = list.SerializeToXml();
            var deserializedList = serializedString.DeserializeFromXml<List<string>>();

            // Assert
            Assert.NotNull(serializedString);
            Assert.NotNull(deserializedList);
            Assert.True(deserializedList.Count == 3);
            Assert.True(deserializedList[0] == "a");
            Assert.True(deserializedList[1] == "b");
            Assert.True(deserializedList[2] == "c");
        }

        [Fact]
        public void ShouldNotDeserializeAnInterfaceType()
        {
            // Arrange
            IList<string> list = new List<string> { "a", "b", "c" };

            // Act
            var serializedString = list.SerializeToXml();
            Assert.Throws<ArgumentException>(() => serializedString.DeserializeFromXml<IList<string>>());

            // Assert
            serializedString.Should().NotBeNullOrEmpty();
        }
    }
}