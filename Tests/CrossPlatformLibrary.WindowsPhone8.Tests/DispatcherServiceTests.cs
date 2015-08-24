using System.Threading;

using CrossPlatformLibrary.Dispatching;
using CrossPlatformLibrary.IoC;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.WindowsPhone8.Tests
{
    public class DispatcherServiceTests
    {
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
    }
}
