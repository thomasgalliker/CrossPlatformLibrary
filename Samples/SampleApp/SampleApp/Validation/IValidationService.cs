using System.Threading.Tasks;
using SampleApp.Model;

namespace SampleApp.Validation
{
    public interface IValidationService
    {
        Task<ValidationResultDto> ValidatePersonAsync(PersonDto personDto);
    }
}