namespace CrossPlatformLibrary.Services
{
    public interface IAppHandler
    {
        bool LaunchApp(string uri);

        bool OpenAppSettings();

        bool OpenLocationServiceSettings();
    }
}