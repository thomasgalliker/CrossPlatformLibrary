using CrossPlatformLibrary.Mvvm;
using SampleApp.Model;

namespace SampleApp.ViewModels
{
    public class CountryViewModel : BindableBase
    {
        private bool isSelected;

        public CountryViewModel(CountryDto countryDto)
        {
            this.Id = countryDto.Id;
            this.Name = countryDto.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public bool IsSelected
        {
            get => this.isSelected;
            set => this.SetProperty(ref this.isSelected, value, nameof(this.IsSelected));
        }
    }
}