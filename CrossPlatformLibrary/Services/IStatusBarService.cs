namespace CrossPlatformLibrary.Services
{
    public interface IStatusBarService
    {
        void SetHexColor(string hexColor);

        void SetStatusBarMode(StatusBarMode statusBarMode);
    }

    public enum StatusBarMode
    {
        Light,
        Dark
    }
}
