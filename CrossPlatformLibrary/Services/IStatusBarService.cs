namespace CrossPlatformLibrary.Services
{
    public interface IStatusBarService
    {
        void SetHexColor(string hexColor);

        void SetStatusBarMode(StatusBarStyle statusBarMode);
    }

    public enum StatusBarStyle
    {
        /// <summary>
        /// Status bar style 'Light' for use with bright status bar colors.
        /// </summary>
        Light,

        /// <summary>
        /// Status bar style 'Dark' for use with dark status bar colors.
        /// </summary>
        Dark
    }
}
