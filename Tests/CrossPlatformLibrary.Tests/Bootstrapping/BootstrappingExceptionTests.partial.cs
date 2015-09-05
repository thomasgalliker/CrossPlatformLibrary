using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Roche.Comit.Utilities.UnitTesting;

namespace Roche.LabCore.Bootstrapping
{
    public partial class BootstrappingExceptionTests
    {
        [TestMethod]
        public void IsSerializable()
        {
            BootstrappingException exception = new BootstrappingException();
            BootstrappingException serializedException = SerializationAssert.IsBinarySerializable(exception);
            Assert.AreEqual(exception.Message, serializedException.Message);
            Assert.AreEqual(exception.InnerException, serializedException.InnerException);
        }

        [TestMethod]
        public void PropertiesAreSerializedCorrectly()
        {
            BootstrappingException exception = new BootstrappingException("message");
            BootstrappingException serializedException = SerializationAssert.IsBinarySerializable(exception);
            Assert.AreEqual(exception.Message, serializedException.Message);
            Assert.AreEqual(exception.InnerException, serializedException.InnerException);

            exception = new BootstrappingException("message", new BootstrappingException());
            serializedException = SerializationAssert.IsBinarySerializable(exception);
            Assert.AreEqual(exception.Message, serializedException.Message);
            Assert.AreEqual(exception.InnerException.Message, serializedException.InnerException.Message);
            Assert.AreEqual(exception.InnerException.InnerException, serializedException.InnerException.InnerException);
        }
    }
}