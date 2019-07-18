using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SampleApp.Model;

namespace SampleApp.Services
{
    public class CountryService : ICountryService
    {
        private readonly HttpClient httpClient;

        public CountryService()
        {
            this.httpClient = new HttpClient();
            //this.httpClient.BaseAddress = new Uri("https://restcountries.eu/rest/v2/all");

            // Add an Accept header for JSON format.
            this.httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public async Task<IEnumerable<CountryDto>> GetAllAsync()
        {
            var response = await this.httpClient.GetAsync("https://restcountries.eu/rest/v2/all");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var countries = JsonConvert.DeserializeObject<IEnumerable<CountryDto>>(content);
            return countries;
        }
    }
}