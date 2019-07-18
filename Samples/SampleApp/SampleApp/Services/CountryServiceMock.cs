using System.Collections.Generic;
using System.Threading.Tasks;
using SampleApp.Model;

namespace SampleApp.Services
{
    public class CountryServiceMock : ICountryService
    {
        public Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<CountryDto>>(new List<CountryDto>
            {

                new CountryDto{ Id = 1, Name = "Switzerland"},
                new CountryDto{ Id = 2, Name = "Germany"},
                new CountryDto{ Id = 3, Name = "France"},
                new CountryDto{ Id = 4, Name = "French Guiana"},
                new CountryDto{ Id = 5, Name = "French Polynesia"},
                new CountryDto{ Id = 6, Name = "French Southern Territories (the)"},
                new CountryDto{ Id = 7, Name = "USA"}
            });
        }
    }
}