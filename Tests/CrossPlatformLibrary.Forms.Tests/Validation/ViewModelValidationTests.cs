using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrossPlatformLibrary.Forms.Tests.Testdata;
using FluentAssertions;
using Moq;
using SampleApp.Model;
using SampleApp.Validation;
using Xunit;
using Xunit.Abstractions;

namespace CrossPlatformLibrary.Forms.Tests.Validation
{
    public class ViewModelValidationTests
    {
        private readonly ITestOutputHelper testOutputHelper;
        private readonly Mock<IValidationService> validationServiceMock;

        public ViewModelValidationTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
            this.validationServiceMock = new Mock<IValidationService>();
            this.validationServiceMock.Setup(v => v.ValidatePersonAsync(It.IsAny<PersonDto>())).ReturnsAsync(new ValidationResultDto());
        }

        [Fact]
        public void ShouldBeNullIfNotInitialized()
        {
            // Arrange / Act
            var testViewModel = new EmptyTestViewModel();

            // Assert
            testViewModel.Validation.Should().BeNull();
        }

        [Fact]
        public void ShouldThrowIfNoValidationsExist()
        {
            // Arrange
            var testViewModel = new NoValidationsTestViewModel();

            // Act
            Func<Task> action = async () => await testViewModel.Validation.IsValidAsync();

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task ShouldAddValidationFor_IsValidFalse_NoValuesProvided()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object);

            // Act
            var isValid = await testViewModel.Validation.IsValidAsync();

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
        public async Task ShouldAddValidationFor_IsValidFalse_WithParametersInErrorMessage()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                Email = "thomasbluewin.ch"
            };

            // Act
            var isValid = await testViewModel.Validation.IsValidAsync();

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
        public async Task ShouldAddValidationFor_IsValidTrue()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };

            // Act
            var isValid = await testViewModel.Validation.IsValidAsync();

            // Assert
            isValid.Should().BeTrue();

            testViewModel.Validation.Errors.HasErrors.Should().BeFalse();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(0);
        }

        [Fact]
        public async Task ShouldAddValidationFor_IsValidTrue_AutoReevaluate()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                UserName = "thomas",
                Email = "thomasbluewin.ch"
            };
            var isValid = await testViewModel.Validation.IsValidAsync();
            isValid.Should().BeFalse();

            // Act
            testViewModel.Email = "thomas@bluewin.ch";

            // Assert
            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(0);
        }

        [Fact]
        public async Task ShouldAddValidation_IsValidTrue()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };

            // Act
            var isValid = await testViewModel.Validation.IsValidAsync();

            // Assert
            isValid.Should().BeTrue();

            testViewModel.Validation.Errors.HasErrors.Should().BeFalse();
            testViewModel.Validation.Errors[nameof(TestViewModel.Email)].Should().HaveCount(0);
        }

        [Fact]
        public async Task ShouldAddErrorMessageForProperty_IsValidFalse()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };
            await testViewModel.Validation.IsValidAsync();

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
        public async Task ShouldAddErrorMessageForProperty_IsValidTrue_AfterClearErrorMessages()
        {
            // Arrange
            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };
            var isValid1 = await testViewModel.Validation.IsValidAsync();

            var hasErrors1 = testViewModel.Validation.HasErrors;
            testViewModel.Validation.AddErrorMessageForProperty(nameof(TestViewModel.Email), "Some backend validation error");
            var hasErrors2 = testViewModel.Validation.HasErrors;

            // Act
            var isValid2 = await testViewModel.Validation.IsValidAsync();

            // Assert
            isValid1.Should().BeTrue(because: "initially, we're all green");
            hasErrors1.Should().BeFalse(because: "initially, we're all green");
            hasErrors2.Should().BeTrue(because: "we add a validation error manually");
            isValid2.Should().BeTrue(because: "IsValid clears all manually added error messages");

            testViewModel.Validation.Errors.HasErrors.Should().BeFalse();
        }

        [Fact]
        public async Task ShouldAddValidation_IsValidFalse()
        {
            // Arrange
            var validationResultDto = new ValidationResultDto
            {
                Errors = new Dictionary<string, List<string>>
                {
                    { nameof(TestViewModel.UserName), new List<string> { "User 'thomas' already exists" } }
                }
            };

            this.validationServiceMock.Setup(v => v.ValidatePersonAsync(It.IsAny<PersonDto>()))
                .ReturnsAsync(validationResultDto);

            var testViewModel = new TestViewModel(this.validationServiceMock.Object)
            {
                UserName = "thomas",
                Email = "thomas@bluewin.ch"
            };

            // Act
            var isValid = await testViewModel.Validation.IsValidAsync();

            // Assert
            isValid.Should().BeFalse();

            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.UserName)].Should().HaveCount(1);
            testViewModel.Validation.Errors[nameof(TestViewModel.UserName)].Should().ContainInOrder(new[]
            {
                "User 'thomas' already exists"
            });
        }

        [Fact]
        public async Task ShouldHandleThreadSafeParallelValidations()
        {
            // Arrange
            const int numberOfValidations = 10;


            this.validationServiceMock.Setup(v => v.ValidatePersonAsync(It.IsAny<PersonDto>()))
                .ReturnsAsync((PersonDto p) => this.CreateValidationResult(p));

            var testViewModel = new ParallelTestViewModel(this.validationServiceMock.Object, numberOfValidations)
            {
                UserName = "thomas",
            };

            // Act
            var isValidTasks = Enumerable.Range(0, 100).Select(i => testViewModel.Validation.IsValidAsync());
            var isValidTaskResults = await Task.WhenAll(isValidTasks);

            // Assert
            foreach (var error in testViewModel.Validation.GetErrors().OrderBy(e => e.Key))
            {
                foreach (var message in error.Value.OrderBy(m => m))
                {
                    this.testOutputHelper.WriteLine($"{error.Key} -> {message}");
                }
            }

            this.testOutputHelper.WriteLine(ObjectDumper.Dump(isValidTaskResults, DumpStyle.CSharp));
            isValidTaskResults.Last().Should().BeFalse();

            testViewModel.Validation.Errors.HasErrors.Should().BeTrue();
            testViewModel.Validation.Errors[nameof(TestViewModel.UserName)].Should().HaveCount(numberOfValidations);
            testViewModel.Validation.Errors[nameof(TestViewModel.UserName)].Should().Contain(Enumerable.Range(0, numberOfValidations).Select(i => $"Random validation error for person {i}"));
        }

        private ValidationResultDto CreateValidationResult(PersonDto personDto)
        {
            return new ValidationResultDto
            {
                Errors = new Dictionary<string, List<string>>
                {
                    { nameof(TestViewModel.UserName), new List<string> { $"Random validation error for person {personDto.Id}" } }
                }
            };
        }
    }
}