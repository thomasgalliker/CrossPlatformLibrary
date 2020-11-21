using System.Threading.Tasks;

namespace SampleApp.Services
{
    public interface IDisplayService
    {
        Task DisplayAlert(string title, string message);
    }
}