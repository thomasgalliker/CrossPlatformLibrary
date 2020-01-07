using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Effects
{
    public class SafeAreaPaddingEffect : RoutingEffect
    {
        public SafeAreaPaddingEffect() :
            base(SafeAreaPadding.EffectName)
        {
        }
    }
}