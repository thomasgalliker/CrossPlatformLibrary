using System;
using System.Threading.Tasks;


using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.Utils
{
    [Collection("AsyncInline")]
    public class AsyncInlineTests
    {
        [Fact]
        public void ShouldRunAsyncInline()
        {
            // Arrange
            bool taskCalled = false;

            // Act
            AsyncInline.Run(() => Task.Factory.StartNew(() => { taskCalled = true; }));

            // Assert
            taskCalled.Should().BeTrue();
        }

        [Fact]
        public void ShouldRunAsyncInlineWithResult()
        {
            // Arrange
            bool taskCalled = false;

            // Act
            taskCalled = AsyncInline.Run(() => Task.Factory.StartNew(() => true));

            // Assert
            taskCalled.Should().BeTrue();
        }

        [Fact]
        public void ShouldRunAsyncInlineWithException()
        {
            // Arrange
            var invalidOperationException = new InvalidOperationException("This test must fail.");

            // Act
            Action action = () => AsyncInline.Run(() => Task.Factory.StartNew(() => { throw invalidOperationException; }));


            // Assert
            Assert.Throws<AggregateException>(action);
        }
    }
}
