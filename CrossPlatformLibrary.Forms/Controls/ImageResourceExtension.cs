using System;
using System.Reflection;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Controls
{
    [ContentProperty("Source")]
    public class ImageResourceExtension : IMarkupExtension
    {
        public string Source { get; set; }

        public string AssemblyName { get; set; }

        private static Assembly ResourceAssembly = null;

        public static void Init(Assembly resourceAssembly)
        {
            ResourceAssembly = resourceAssembly;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Source == null)
            {
                return null;
            }

            var imageSource = ImageSource.FromResource(this.Source, ResourceAssembly);
            return imageSource;
        }
    }
}