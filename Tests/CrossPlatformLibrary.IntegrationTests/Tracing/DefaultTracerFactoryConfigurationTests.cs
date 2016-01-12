using CrossPlatformLibrary.IoC;

using Xunit;
using CrossPlatformLibrary.Tracing;
using FluentAssertions;

namespace CrossPlatformLibrary.IntegrationTests.Tracing
{
    [Trait("Category", "IntegrationTests")]
    public class DefaultTracerFactoryConfigurationTests
    {
        [Fact]
        public void ShouldGetInstanceOfDefaultTracerFactoryConfiguration()
        {
            // Act
            var platformServices = SimpleIoc.Default.GetInstance<IDefaultTracerFactoryConfiguration>();

            // Assert
            platformServices.Should().NotBeNull();
        }
    }
}
