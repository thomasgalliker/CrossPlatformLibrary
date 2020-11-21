using System;
using System.Collections.Generic;
using CrossPlatformLibrary.Forms.Converters;
using CrossPlatformLibrary.Forms.Validation;
using CrossPlatformLibrary.Forms.Validation.Converters;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Validation.Converters
{
    public class ValidationErrorsToStringConverterTests
    {
        [Fact]
        public void ShouldConvert_SingleProperty_DefaultBullets()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsToStringConverter { ShowBullets = true };
            var input = new ViewModelValidation();
            input.AddErrorMessageForProperty("TestProperty1", "Error message 1");
            input.AddErrorMessageForProperty("TestProperty2", "Error message 2");

            // Act
            var convertedOutput = converter.Convert(input, null, null, null);

            // Assert
            convertedOutput.Should().NotBeNull();
            convertedOutput.Should().Be("• Error message 1\r\n• Error message 2");
        }

        [Fact]
        public void ShouldConvert_SingleProperty_CustomBullets()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsToStringConverter { ShowBullets = true, Bullet = "- "};
            var input = new ViewModelValidation();
            input.AddErrorMessageForProperty("TestProperty1", "Error message 1");
            input.AddErrorMessageForProperty("TestProperty2", "Error message 2");

            // Act
            var convertedOutput = converter.Convert(input, null, null, null);

            // Assert
            convertedOutput.Should().NotBeNull();
            convertedOutput.Should().Be("- Error message 1\r\n- Error message 2");
        }
        
        [Fact]
        public void ShouldConvert_SingleProperty_NoBullets()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsToStringConverter { ShowBullets = false };
            var input = new ViewModelValidation();
            input.AddErrorMessageForProperty("TestProperty1", "Error message 1");
            input.AddErrorMessageForProperty("TestProperty2", "Error message 2");

            // Act
            var convertedOutput = converter.Convert(input, null, null, null);

            // Assert
            convertedOutput.Should().NotBeNull();
            convertedOutput.Should().Be("Error message 1\r\nError message 2");
        }

        [Fact]
        public void ShouldConvertBack_ThrowsException()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsToStringConverter();
            var input = new ViewModelValidation();

            // Act
            Action action = () => converter.ConvertBack(input, null, null, null);

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}