using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Validation;
using SampleApp.Model;
using SampleApp.Validation;

namespace CrossPlatformLibrary.Forms.Tests.Testdata
{
    public class ParallelTestViewModel : BaseViewModel
    {
        private string userName;

        public ParallelTestViewModel(IValidationService validationService, int numberOfValidations)
        {
            for (var i = 0; i < numberOfValidations; i++)
            {
                var personId = i;
                this.Validation.AddDelegateValidation(nameof(this.UserName))
                    .Show(async () => (await validationService.ValidatePersonAsync(this.CreatePerson(personId))).Errors);
            }

        }

        public string UserName
        {
            get => this.userName;
            set => this.SetProperty(ref this.userName, value, nameof(this.UserName));
        }

        protected override ViewModelValidation SetupValidation()
        {
            var viewModelValidation = new ViewModelValidation();
            return viewModelValidation;
        }

        private PersonDto CreatePerson(int id)
        {
            return new PersonDto
            {
                Id = id,
                UserName = this.UserName,
            };
        }
    }
}