using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CrossPlatformLibrary.Forms.Validation;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Validation
{
    public class DelegateValidationTests
    {
        [Fact]
        public void ShouldThrowIfNoValidationDelegationIsSetup()
        {
            // Arrange
            var delegateValidation = new DelegateValidation(new[] { "TestProperty1" });

            // Act
            Func<Task> action = async () => await delegateValidation.GetErrors();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task ShouldGetErrorsWithEmptyResult()
        {
            // Arrange
            var delegateValidation = new DelegateValidation(new[] { "TestProperty1" });
            delegateValidation.Validate(() => Task.FromResult(new Dictionary<string, List<string>>()));

            // Act
            var errors = await delegateValidation.GetErrors();

            // Assert
            errors.Should().NotBeNull();
            errors.Should().BeEmpty();
        }

        [Fact]
        public async Task ShouldGetErrorsWithDelay_SingleTask()
        {
            // Arrange
            var propertyName = "TestProperty1";
            var delegateValidation = new DelegateValidation(new[] { propertyName })
                .Validate(() => ValidateAsync(propertyName))
                .WithDelay(TimeSpan.FromMilliseconds(1000));

            // Act
            var errors1 = await delegateValidation.GetErrors();

            // Assert
            errors1.Should().NotBeNull();
            errors1.Should().NotBeEmpty();
        }

        [Fact]
        public async Task ShouldGetErrorsWithDelay_MultipleTasks()
        {
            // Arrange
            var propertyName = "TestProperty1";
            var delegateValidation = new DelegateValidation(new[] { propertyName })
                .Validate(() => ValidateAsync(propertyName))
                .WithDelay(TimeSpan.FromMilliseconds(1000));

            // Act
            var errorsTask1 = delegateValidation.GetErrors();
            var errorsTask2 = delegateValidation.GetErrors();

            var errors1 = await errorsTask1;
            await Task.Delay(200);
            var errors2 = await errorsTask2;

            // Assert
            errors1.Should().NotBeNull();
            errors1.Should().BeEmpty(because: "this task must be cancelled");

            errors2.Should().NotBeNull();
            errors2.Should().HaveCount(1);
        }

        private static async Task<Dictionary<string, List<string>>> ValidateAsync(string propertyName)
        {
            await Task.Delay(200);
            return new Dictionary<string, List<string>>
            {
                { propertyName, new List<string>{ "Error1"}}
            };
        }
    }
}
