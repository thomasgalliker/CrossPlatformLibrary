using CrossPlatformLibrary.Forms.Tests.Testdata;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Validation
{
    public class ViewModelValidationTests
    {
        [Fact]
        public void ShouldAddValidationFor_IsValidFalse_NoValuesProvided()
        {
            // Arrange
            var testViewModel = new TestViewModel();

            // Act
            var isValid = testViewModel.Validation.IsValid();

            // Assert
            isValid.Should().BeFalse();

            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.UserName)].Should().HaveCount(2);
            testViewModel.Validation.Errors[nameof(TestViewModel.UserName)].Should().ContainInOrder(new[]
            {
                "Username must not be empty (function).",
                "Username must not be empty (static)."
            });

            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(4);
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().ContainInOrder(new[]
            {
                "Email address '' must not be empty.",
                "Domain is black-listed.",
                "Email address '' is not valid.",
                "Your mail address '' is not valid."
            });
        }

        [Fact]
        public void ShouldAddValidationFor_IsValidFalse_WithParametersInErrorMessage()
        {
            // Arrange
            var testViewModel = new TestViewModel
            {
                Email = "thomasbluewin.ch"
            };

            // Act
            var isValid = testViewModel.Validation.IsValid();

            // Assert
            isValid.Should().BeFalse();

            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(2);
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().ContainInOrder(new[]
            {
                "Email address 'thomasbluewin.ch' is not valid.",
                "Your mail address 'thomasbluewin.ch' is not valid."
            });
        }

        [Fact]
        public void ShouldAddValidationFor_IsValidTrue()
        {
            // Arrange
            var testViewModel = new TestViewModel
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };

            // Act
            var isValid = testViewModel.Validation.IsValid();

            // Assert
            isValid.Should().BeTrue();

            testViewModel.Validation.Errors.HasErrors.Should().BeFalse();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(0);
        }

        [Fact]
        public void ShouldAddValidationFor_IsValidTrue_AutoReevaluate()
        {
            // Arrange
            var testViewModel = new TestViewModel
            {
                UserName = "thomas",
                Email = "thomasbluewin.ch"
            };
            var isValid = testViewModel.Validation.IsValid();
            isValid.Should().BeFalse();

            // Act
            testViewModel.Email = "thomas@bluewin.ch";

            // Assert
            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(0);
        }

        [Fact]
        public void ShouldAddErrorMessageForProperty_IsValidFalse()
        {
            // Arrange
            var testViewModel = new TestViewModel
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };
            testViewModel.Validation.IsValid();

            // Act
            testViewModel.Validation.AddErrorMessageForProperty(nameof(TestViewModel.Email), "Some backend validation error");

            // Assert
            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(1);
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().ContainInOrder(new[]
            {
                "Some backend validation error"
            });
        }

        [Fact]
        public void ShouldAddErrorMessageForProperty_IsValidTrue_AfterClearErrorMessages()
        {
            // Arrange
            var testViewModel = new TestViewModel
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };
            var isValid1 = testViewModel.Validation.IsValid();

            var hasErrors1 = testViewModel.Validation.HasErrors;
            testViewModel.Validation.AddErrorMessageForProperty(nameof(TestViewModel.Email), "Some backend validation error");
            var hasErrors2 = testViewModel.Validation.HasErrors;

            // Act
            var isValid2 = testViewModel.Validation.IsValid();

            // Assert
            isValid1.Should().BeTrue(because: "initially, we're all green");
            hasErrors1.Should().BeFalse(because: "initially, we're all green");
            hasErrors2.Should().BeTrue(because: "we add a validation error manually");
            isValid2.Should().BeTrue(because: "IsValid clears all manually added error messages");

            testViewModel.Validation.Errors.HasErrors.Should().BeFalse();
        }
    }
}