using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Themes
{
    public interface ISpacingConfiguration
    {
        double SmallSpacing { get; set; }

        Thickness SmallPadding { get; set; }

        double MediumSpacing { get; set; }

        Thickness MediumPadding { get; set; }

        double LargeSpacing { get; set; }

        Thickness LargePadding { get; set; }

        Thickness CardViewPadding { get; set; }

        Thickness CardPadding { get; set; }

        double CardSpacing { get; set; }
    }
}