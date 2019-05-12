using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms
{
    public static class CrossPlatformLibrary
    {
        private static App app;

        public static ResourceDictionary Init()
        {
            app = new App();
            return app.Resources;
        }
    }
}
