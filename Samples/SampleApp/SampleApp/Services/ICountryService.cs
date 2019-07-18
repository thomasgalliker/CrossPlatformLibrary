using System.Collections.Generic;
using System.Threading.Tasks;
using SampleApp.Model;

namespace SampleApp.Services
{
    public interface ICountryService
    {
        Task<IEnumerable<CountryDto>> GetAllAsync();
    }
}