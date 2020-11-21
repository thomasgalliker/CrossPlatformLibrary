using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyResourceDictionary : ResourceDictionary
    {
        public MyResourceDictionary()
        {
            this.InitializeComponent();
        }
    }
}