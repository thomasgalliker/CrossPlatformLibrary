namespace CrossPlatformLibrary.Services
{
    /// <summary>
    ///     Clipboard service which can be used to read/write text from/to the clipboard.
    /// </summary>
    public interface IClipboardService
    {
        /// <summary>
        ///     Sets <paramref name="text" /> to clipboard.
        /// </summary>
        void SetText(string text);

        /// <summary>
        ///     Gets text from clipboard.
        /// </summary>
        string GetText();
    }
}