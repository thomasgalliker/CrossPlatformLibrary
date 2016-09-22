using System;
using System.Threading;

using CrossPlatformLibrary.Bootstrapping;
using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.IoC;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.IntegrationTests.Dispatching
{
    [Trait("Category", "IntegrationTests")]
    public class DispatcherServiceTests : IDisposable
    {
        private readonly Bootstrapper bootstrapper;

        public DispatcherServiceTests()
        {
            this.bootstrapper = new Bootstrapper();
            this.bootstrapper.Startup();
        }

        [Fact]
        public void ShouldGetInstanceOfDispatcherService()
        {
            // Arrange
            ManualResetEvent sync = new ManualResetEvent(false);
            bool beginInvokeCalled = false;
            var dispatcherService = SimpleIoc.Default.GetInstance<IDispatcherService>();
            dispatcherService.Should().NotBeNull();

            // Act
            dispatcherService.CheckBeginInvokeOnUI(
                () =>
                    {
                        beginInvokeCalled = true;
                        sync.Set();
                    });

            if (!sync.WaitOne(1000))
            {
                Assert.True(false, "Test timed out");
            }

            // Assert
            beginInvokeCalled.Should().BeTrue();
        }

        public void Dispose()
        {
            this.bootstrapper.Shutdown();
        }
    }
}