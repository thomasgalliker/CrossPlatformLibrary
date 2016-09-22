
using System;

using CrossPlatformLibrary.Bootstrapping;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.IntegrationTests.Bootstrapping
{
    [Trait("Category", "IntegrationTests")]
    public class BootstrapperTests
    {
        [Fact]
        public void ShouldStartupBootstrapperInBackgroundThread()
        {
            // Arrange
            var bootstrapper = new Bootstrapper();

            // Act
            Action startupAction = () =>
                {
                    // The unit test framework is run in a background thread
                    // Bootstrapper.Startup should survive this situation without exception
                    bootstrapper.Startup();
                };

            // Assert
            startupAction.ShouldNotThrow();
        }
    }
}
