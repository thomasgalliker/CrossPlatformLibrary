
using System;

using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.IoC;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.IntegrationTests.ExceptionHandling
{
    [Trait("Category", "IntegrationTests")]
    public class ExceptionHandlerTests : IDisposable
    {
        private readonly Bootstrapper bootstrapper;

        public ExceptionHandlerTests()
        {
            this.bootstrapper = new Bootstrapper();
            this.bootstrapper.Startup();
        }

        [Fact]
        public void ShouldGetInstanceOfExceptionHandler()
        {
            // Act
            var exceptionHandler = SimpleIoc.Default.GetInstance<IExceptionHandler>();

            // Assert
            exceptionHandler.Should().NotBeNull();
        }

        [Fact]
        public void ShouldGetInstanceOfPlatformSpecificExceptionHandler()
        {
            // Act
            var platformSpecificExceptionHandler = SimpleIoc.Default.GetInstance<IExceptionHandler>();

            // Assert
            platformSpecificExceptionHandler.Should().NotBeNull();
        }

        public void Dispose()
        {
            this.bootstrapper.Shutdown();
        }
    }
}
