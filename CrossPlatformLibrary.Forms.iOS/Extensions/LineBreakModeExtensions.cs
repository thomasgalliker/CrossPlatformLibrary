using UIKit;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.iOS.Extensions
{
    public static partial class LineBreakModeExtensions
    {
        public static UILineBreakMode ToUILineBreakMode(this LineBreakMode mode)
        {
            switch (mode)
            {
                case LineBreakMode.CharacterWrap:
                    return UILineBreakMode.CharacterWrap;
                case LineBreakMode.HeadTruncation:
                    return UILineBreakMode.HeadTruncation;
                case LineBreakMode.MiddleTruncation:
                    return UILineBreakMode.MiddleTruncation;
                case LineBreakMode.TailTruncation:
                    return UILineBreakMode.TailTruncation;
                case LineBreakMode.WordWrap:
                    return UILineBreakMode.WordWrap;
                case LineBreakMode.NoWrap:
                default:
                    return UILineBreakMode.Clip;
            }
        }
    }
}