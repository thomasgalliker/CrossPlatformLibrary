using CrossPlatformLibrary.Forms.Controls;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Services
{
    public interface IActivityIndicatorService
    {
        void Init(ContentPage activityIndicatorPage);

        void ShowLoadingPage(string text);

        void HideLoadingPage();
    }
}