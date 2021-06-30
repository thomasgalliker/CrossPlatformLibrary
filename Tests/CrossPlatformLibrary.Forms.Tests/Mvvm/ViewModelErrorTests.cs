using CrossPlatformLibrary.Forms.Mvvm;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Mvvm
{
    public class ViewModelErrorTests
    {
        [Fact]
        public void ShouldCallActionIfCommandIsExecuted()
        {
            // Arrange
            var actionCalls = 0;
            var viewModelError = new ViewModelError("text", () => { actionCalls++; });

            // Act
            viewModelError.Command.Execute(null);

            // Assert
            actionCalls.Should().Be(1);
            viewModelError.Text.Should().Be("text");
        }
    }
}
