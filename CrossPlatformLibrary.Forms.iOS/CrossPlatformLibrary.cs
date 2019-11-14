using CrossPlatformLibrary.Forms.iOS.Services;

namespace CrossPlatformLibrary.Forms.iOS
{
    public static class CrossPlatformLibrary
    {
        /// <summary>
        /// Initializes the platform-specific parts of this library.
        /// </summary>
        public static void Init()
        {
            global::CrossPlatformLibrary.Forms.CrossPlatformLibrary.SetFontConverter(new FontConverter());
        }
    }
}
