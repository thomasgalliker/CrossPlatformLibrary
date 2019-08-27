using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms
{
    /// <summary>
    /// Class that provides Material theme configuration that will be applied in the current App.
    /// </summary>
    public class CrossPlatformLibraryConfiguration : BindableObject, ITheme
    {
        /// <summary>
        /// Backing field for the bindable property <see cref="ColorConfiguration"/>.
        /// </summary>
        public static readonly BindableProperty ColorConfigurationProperty =
            BindableProperty.Create(
                nameof(ColorConfiguration),
                typeof(CrossPlatformLibraryColorConfiguration),
                typeof(CrossPlatformLibraryColorConfiguration));

        /// <summary>
        /// Gets or sets the color configuration of the theme.
        /// </summary>
        public CrossPlatformLibraryColorConfiguration ColorConfiguration
        {
            get => (CrossPlatformLibraryColorConfiguration)this.GetValue(ColorConfigurationProperty);
            set => this.SetValue(ColorConfigurationProperty, value);
        }
    }

    public interface ITheme
    {
        CrossPlatformLibraryColorConfiguration ColorConfiguration { get; set; }
    }
}