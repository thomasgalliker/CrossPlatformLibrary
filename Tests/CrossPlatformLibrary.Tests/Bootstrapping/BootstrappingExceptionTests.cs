using System;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Roche.Comit.Utilities.UnitTesting;

namespace Roche.LabCore.Bootstrapping
{
    [TestClass]
    public partial class BootstrappingExceptionTests
    {
        [TestMethod]
        public void PropertiesAreInitializedCorrectly()
        {
            BootstrappingException exception = new BootstrappingException();
            Assert.IsNotNull(exception.Message);
            Assert.IsNull(exception.InnerException);

            exception = new BootstrappingException("message");
            Assert.AreEqual("message", exception.Message);
            Assert.IsNull(exception.InnerException);

            BootstrappingException innerException = new BootstrappingException();
            exception = new BootstrappingException("message", innerException);
            Assert.AreEqual("message", exception.Message);
            Assert.AreSame(innerException, exception.InnerException);
        }

        [TestMethod]
        public void NullOrEmptyMessageNotThrows()
        {
            ExceptionAssert.IsNotThrown<Exception>(() => new BootstrappingException(null));
            ExceptionAssert.IsNotThrown<Exception>(() => new BootstrappingException(string.Empty));
        }

        [TestMethod]
        public void NullInnerExceptionNotThrows()
        {
            ExceptionAssert.IsNotThrown<Exception>(() => new BootstrappingException(null, null));
        }
    }
}