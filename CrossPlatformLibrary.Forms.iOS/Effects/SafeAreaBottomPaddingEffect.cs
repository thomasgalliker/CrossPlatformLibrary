using CrossPlatformLibrary.Forms.iOS.Effects;
using CrossPlatformLibrary.Internals;
using Xamarin.Forms;

[assembly: ExportEffect(typeof(SafeAreaBottomPaddingEffect), nameof(SafeAreaBottomPaddingEffect))]

namespace CrossPlatformLibrary.Forms.iOS.Effects
{
    /// <summary>
    ///     Adds safe-area padding to the bottom of the UI element.
    /// </summary>
    public class SafeAreaBottomPaddingEffect : SafeAreaPaddingEffect
    {
        private readonly ITracer tracer;

        public SafeAreaBottomPaddingEffect()
        {
            this.tracer = Tracer.Current;
        }

        protected override Thickness GetSafeAreaPadding(Thickness originalPadding, Thickness safeAreaInsets, bool includeStatusBar)
        {
            var safeAreaPadding = base.GetSafeAreaPadding(originalPadding, safeAreaInsets, includeStatusBar: false);
            safeAreaPadding.Top = 0;

            this.tracer.Info($"SafeAreaBottomPaddingEffect.GetSafeAreaPadding returns safeAreaPadding={{{safeAreaPadding.Left}, {safeAreaPadding.Top}, {safeAreaPadding.Right}, {safeAreaPadding.Bottom}}}");
            return safeAreaPadding;
        }
    }
}