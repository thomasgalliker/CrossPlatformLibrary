using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Services
{
    public interface IActivityIndicatorService
    {
        void Init<T>(T activityIndicatorPage) where T : ContentPage, IActivityIndicatorPage;

        void ShowLoadingPage(string text);

        void HideLoadingPage();
    }
}