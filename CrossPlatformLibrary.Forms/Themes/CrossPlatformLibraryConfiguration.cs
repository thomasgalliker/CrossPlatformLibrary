using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    /// <summary>
    /// Class that provides the theme configuration that will be applied in the current App.
    /// </summary>
    public class CrossPlatformLibraryConfiguration : BindableObject, ITheme
    {
        public static readonly BindableProperty ColorConfigurationProperty =
            BindableProperty.Create(
                nameof(ColorConfiguration),
                typeof(IColorConfiguration),
                typeof(CrossPlatformLibraryConfiguration));

        /// <summary>
        /// Gets or sets the color configuration of the theme.
        /// </summary>
        public IColorConfiguration ColorConfiguration
        {
            get => (IColorConfiguration)this.GetValue(ColorConfigurationProperty);
            set => this.SetValue(ColorConfigurationProperty, value);
        }

        public static readonly BindableProperty SpacingConfigurationProperty =
            BindableProperty.Create(
                nameof(SpacingConfiguration),
                typeof(SpacingConfiguration),
                typeof(CrossPlatformLibraryConfiguration));

        /// <summary>
        /// Gets or sets the spacing configuration of the theme.
        /// </summary>
        public ISpacingConfiguration SpacingConfiguration
        {
            get => (ISpacingConfiguration)this.GetValue(SpacingConfigurationProperty);
            set => this.SetValue(SpacingConfigurationProperty, value);
        }
    }

    public interface ITheme
    {
        IColorConfiguration ColorConfiguration { get; set; }

        ISpacingConfiguration SpacingConfiguration { get; set; }
    }
}