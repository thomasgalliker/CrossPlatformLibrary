using SampleApp.Model;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class CountryViewModel : BindableObject
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