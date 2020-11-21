using System.Collections.Generic;
using System.Linq;
using CrossPlatformLibrary.Extensions;
using Xamarin.Forms;

namespace SampleApp.ViewModels
{
    public class ResourceViewModel
    {
        public ResourceViewModel(KeyValuePair<string, object> colorResource)
        {
            this.Key = colorResource.Key;
            this.Value = colorResource.Value;
        }

        public string Key { get; }

        public object Value { get; }

        public string ResourceType
        {
            get
            {
                if (this.Value != null)
                {
                    var type = this.Value.GetType();
                    if (type.GetInterfaces().Contains(typeof(IValueConverter)))
                    {
                        return "IValueConverter";
                    }
                    else
                    {
                        return type.GetFormattedName();
                    }
                }

                return null;
            }
        }

        public override string ToString()
        {
            return this.Key;
        }
    }
}