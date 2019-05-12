using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class CountryViewModel : BindableObject
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}