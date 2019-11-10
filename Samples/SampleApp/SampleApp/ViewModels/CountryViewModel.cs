using SampleApp.Model;

namespace SampleApp.ViewModels
{
    public class CountryViewModel
    {
        public CountryViewModel(CountryDto countryDto)
        {
            this.Id = countryDto.Id;
            this.Name = countryDto.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}