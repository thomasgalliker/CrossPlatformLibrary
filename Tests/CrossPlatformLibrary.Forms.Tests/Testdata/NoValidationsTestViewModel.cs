using CrossPlatformLibrary.Forms.Mvvm;
using CrossPlatformLibrary.Forms.Validation;

namespace CrossPlatformLibrary.Forms.Tests.Testdata
{
    public class NoValidationsTestViewModel : BaseViewModel
    {
        protected override ViewModelValidation SetupValidation()
        {
            return new ViewModelValidation();
        }
    }
}