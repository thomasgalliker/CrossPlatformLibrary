
using CrossPlatformLibrary.ExceptionHandling;
using CrossPlatformLibrary.IoC;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.IntegrationTests.ExceptionHandling
{
    [Trait("Category", "IntegrationTests")]
    public class ExceptionHandlerTests
    {
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
    }
}
