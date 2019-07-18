using System.Diagnostics;
using ResourceDictionaryDemo;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms
{
    public static class CrossPlatformLibrary
    {
        public static void Init(Application app)
        {
            var resources = GetResources();
            foreach (var pair in resources)
            {
                if (app.Resources.ContainsKey(pair.Key))
                {
                    Debug.WriteLine($"Resource with key={pair.Key} already exists");
                }
                else
                {
                    app.Resources.Add(pair.Key, pair.Value);
                }
            }
        }

        public static ResourceDictionary GetResources()
        {
            return new MyResourceDictionary();
        }
    }
}
