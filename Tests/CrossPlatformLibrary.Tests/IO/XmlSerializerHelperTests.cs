using System;
using System.Collections.Generic;

using CrossPlatformLibrary.IO;
using CrossPlatformLibrary.Tests.UnitTesting;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

#if NEWUNITTEST
using Microsoft.VisualStudio.TestPlatform.UnitTestFramework;
#else
#endif

namespace CrossPlatformLibrary.Tests.IO
{
    [TestClass]
    public class XmlSerializerHelperTests
    {
        [TestMethod]
        public void ShouldSerializeAnEmptyObject()
        {
            // Arrange
            object obj = new object();

            // Act
            var serializedString = obj.SerializeToXml();
            var deserializedObject = serializedString.DeserializeFromXml<object>();

            // Assert
            Assert.IsNotNull(serializedString);
            Assert.IsNotNull(deserializedObject);
        }

        [TestMethod]
        public void ShouldSerializeAList()
        {
            // Arrange
            List<string> list = new List<string> { "a", "b", "c" };

            // Act
            var serializedString = list.SerializeToXml();
            var deserializedList = serializedString.DeserializeFromXml<List<string>>();

            // Assert
            Assert.IsNotNull(serializedString);
            Assert.IsNotNull(deserializedList);
            Assert.IsTrue(deserializedList.Count == 3);
            Assert.IsTrue(deserializedList[0] == "a");
            Assert.IsTrue(deserializedList[1] == "b");
            Assert.IsTrue(deserializedList[2] == "c");
        }

        [TestMethod]
        public void ShouldNotDeserializeAnInterfaceType()
        {
            // Arrange
            IList<string> list = new List<string> { "a", "b", "c" };

            // Act
            var serializedString = list.SerializeToXml();
            ExceptionAssert.IsThrown<ArgumentException>(() => serializedString.DeserializeFromXml<IList<string>>());

            // Assert
            serializedString.Should().NotBeNullOrEmpty();
        }
    }
}