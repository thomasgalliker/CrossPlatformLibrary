using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Validation;
using CrossPlatformLibrary.Forms.Validation.Rules;

namespace CrossPlatformLibrary.Forms.Tests.Testdata
{
    public class TestViewModel : BaseViewModel
    {
        private string email;
        private string userName;

        public TestViewModel()
        {
        }

        public string UserName
        {
            get => this.userName;
            set => this.SetProperty(ref this.userName, value, nameof(this.UserName));
        }

        public string Email
        {
            get => this.email;
            set => this.SetProperty(ref this.email, value, nameof(this.Email));
        }

        protected override ViewModelValidation SetupValidation()
        {
            var viewModelValidation = new ViewModelValidation();

            // Validation function with parameter-less custom error message
            viewModelValidation.AddValidationFor(nameof(this.UserName))
                .When(() => string.IsNullOrEmpty(this.UserName))
                .Show(() => "Username must not be empty (function).");

            viewModelValidation.AddValidationFor(nameof(this.UserName))
                .When(() => string.IsNullOrEmpty(this.UserName))
                .Show("Username must not be empty (static).");

            // Validation rule with parameter and custom error message
            viewModelValidation.AddValidationFor(nameof(this.Email))
                .When(new IsNotNullOrEmptyRule())
                .Show(p => $"Email address '{p}' must not be empty.");

            // Validation rule with non-generic IValidationMessage
            viewModelValidation.AddValidationFor(nameof(this.Email))
                .When(new EmailAddressDomainFilterRule());

            // Validation rule with generic IValidationMessage<T>
            viewModelValidation.AddValidationFor(nameof(this.Email))
                .When(new EmailAddressValidationRule());

            // Validation rule with generic IValidationMessage<T> and message override using Show(...)
            viewModelValidation.AddValidationFor(nameof(this.Email))
                .When(new EmailAddressValidationRule())
                .Show(p => $"Your mail address '{p}' is not valid.");

            return viewModelValidation;
        }
    }
}