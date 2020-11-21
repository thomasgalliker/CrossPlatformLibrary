using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using SampleApp.Model;

namespace SampleApp.Validation
{
    public class ValidationService : IValidationService
    {
        public Task<ValidationResultDto> ValidatePersonAsync(PersonDto personDto)
        {
            Debug.WriteLine($"ValidatePersonAsync: {ObjectDumper.Dump(personDto, DumpStyle.CSharp)}");

            var validationResultDto = new ValidationResultDto();
            if (personDto.UserName == null || personDto.UserName.Length < 3)
            {
                validationResultDto.Errors = new Dictionary<string, List<string>>
                {
                    { "UserName", new List<string> { "Username must be at least three characters, otherwise we can't accept it. What about using 'lorem ipsum'?" } }
                };
            }

            if (string.Equals(personDto.UserName, "thomasgalliker", StringComparison.InvariantCultureIgnoreCase))
            {
                validationResultDto.Errors = new Dictionary<string, List<string>>
                {
                    { "UserName", new List<string> { "User already exists!" } }
                };
            }

            return Task.FromResult(validationResultDto);
        }
    }
}