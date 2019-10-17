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

        private static Assembly targetAssembly;

        /// <summary>
        /// Use <seealso cref="Init"/> to define which assembly contains the resources which are resolved using <seealso cref="ImageResourceExtension"/>.
        /// </summary>
        /// <param name="resourceAssembly"></param>
        public static void Init(Assembly resourceAssembly)
        {
            targetAssembly = resourceAssembly;
        }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            if (this.Source == null)
            {
                return null;
            }

            var imageSource = ImageSource.FromResource(this.Source, targetAssembly);
            return imageSource;
        }
    }
}