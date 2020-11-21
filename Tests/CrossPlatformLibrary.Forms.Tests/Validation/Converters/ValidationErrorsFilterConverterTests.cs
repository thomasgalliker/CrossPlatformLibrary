using System;
using System.Collections.Generic;
using CrossPlatformLibrary.Forms.Validation;
using CrossPlatformLibrary.Forms.Validation.Converters;
using FluentAssertions;
using Xamarin.Forms;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Validation.Converters
{
    public class ValidationErrorsFilterConverterTests
    {
        [Fact]
        public void ShouldConvert_SingleProperty()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsFilterConverter();
            var input = new ViewModelValidation();
            input.AddErrorMessageForProperty("TestProperty1", "Error message 1");
            input.AddErrorMessageForProperty("TestProperty2", "Error message 2");

            object parameter = "TestProperty1";

            // Act
            var convertedOutput = converter.Convert(input, null, parameter, null);

            // Assert
            convertedOutput.Should().NotBeNull();

            var propertyErrors = convertedOutput as IEnumerable<string>;
            propertyErrors.Should().ContainInOrder(new[]
            {
                "Error message 1"
            });
        }

        [Fact]
        public void ShouldConvert_MultipleProperties()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsFilterConverter();
            var input = new ViewModelValidation();
            input.AddErrorMessageForProperty("TestProperty1", "Error message 1");
            input.AddErrorMessageForProperty("TestProperty2", "Error message 2");
            input.AddErrorMessageForProperty("TestProperty3", "Error message 3");

            object parameter = "TestProperty1|TestProperty2";

            // Act
            var convertedOutput = converter.Convert(input, null, parameter, null);

            // Assert
            convertedOutput.Should().NotBeNull();

            var propertyErrors = convertedOutput as IEnumerable<string>;
            propertyErrors.Should().ContainInOrder(new[]
            {
                "Error message 1",
                "Error message 2"
            });
        }

        [Fact]
        public void ShouldConvert_ThrowsExceptionIfParameterIsEmpty()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsFilterConverter();
            object input = new ViewModelValidation();

            // Act
            Action action = () => converter.Convert(input, null, null, null);

            // Assert
            action.Should().Throw<InvalidOperationException>().Which.Message.Should().Contain("ConverterParameter must a pipe-delimited string");
        }

        [Fact]
        public void ShouldConvertBack_ThrowsException()
        {
            // Arrange
            IValueConverter converter = new ValidationErrorsFilterConverter();
            var input = new ViewModelValidation();

            // Act
            Action action = () => converter.ConvertBack(input, null, null, null);

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}