using System;
using System.Text;

#if __IOS__ 
    #if __UNIFIED__
        using UIKit;
        using CoreGraphics;
    #else
        using MonoTouch.CoreGraphics;
        using MonoTouch.UIKit;
    #endif
#endif

namespace CrossPlatformLibrary.Media
{
    public struct Color
    {
        public static readonly Color Purple = new Color(0xFFB455B6);
        public static readonly Color Blue = new Color(0xFF3498DB);
        public static readonly Color DarkBlue = new Color(0xFF2C3E50);
        public static readonly Color Green = new Color(0xFF77D065);
        public static readonly Color Gray = new Color(0xFF738182);
        public static readonly Color LightGray = new Color(0xFFB4BCBC);
        public static readonly Color Tan = new Color(0xFFDAD0C8);
        public static readonly Color DarkGray = new Color(0xFF333333);
        public static readonly Color Tint = new Color(0xFF5AA09B);

        // will contain standard 32bit sRGB (ARGB)
        //
        private readonly long value;

        private Color(long value)
        {
            this.value = value;
        }

        /**
         * Shift count and bit mask for A, R, G, B components in ARGB mode!
         */
        private const int ARGBAlphaShift = 24;
        private const int ARGBRedShift = 16;
        private const int ARGBGreenShift = 8;
        private const int ARGBBlueShift = 0;

        public byte R
        {
            get
            {
                return (byte)((this.value >> ARGBRedShift) & 0xFF);
            }
        }

        public byte G
        {
            get
            {
                return (byte)((this.value >> ARGBGreenShift) & 0xFF);
            }
        }

        public byte B
        {
            get
            {
                return (byte)((this.value >> ARGBBlueShift) & 0xFF);
            }
        }

        public byte A
        {
            get
            {
                return (byte)((this.value >> ARGBAlphaShift) & 0xFF);
            }
        }

        public static Color FromArgb(int argb)
        {
            return new Color((long)argb & 0xffffffff);
        }

        public static Color FromArgb(int alpha, int red, int green, int blue)
        {
            CheckByte(alpha, "alpha");
            CheckByte(red, "red");
            CheckByte(green, "green");
            CheckByte(blue, "blue");
            return new Color(MakeArgb((byte)alpha, (byte)red, (byte)green, (byte)blue));
        }

        private static long MakeArgb(byte alpha, byte red, byte green, byte blue)
        {
            return (long)(unchecked((uint)(red << ARGBRedShift | green << ARGBGreenShift | blue << ARGBBlueShift | alpha << ARGBAlphaShift))) & 0xffffffff;
        }

        private static void CheckByte(int value, string name)
        {
            if (value < 0 || value > 255)
            {
                throw new ArgumentException(string.Format("Value {0} for '{1}' out of bounds.", value, name), "value");
            }
        }

        public static implicit operator Color(int hex)
        {
            return FromArgb(hex);
        }

#if __IOS__
		public UIColor ToUIColor ()
		{
			return UIColor.FromRGB ((float)R, (float)G, (float)B);
		}

		public static implicit operator UIColor (Color color)
		{
			return color.ToUIColor ();
		}

		public static implicit operator CGColor (Color color)
		{
			return color.ToUIColor ().CGColor;
		}
#endif

#if __ANDROID__
        public global::Android.Graphics.Color ToAndroidColor()
        {
          return global::Android.Graphics.Color.Rgb((int)(255 * R), (int)(255 * G), (int)(255 * B));
        }

        public static implicit operator global::Android.Graphics.Color(Color color)
        {
            return color.ToAndroidColor();
        }
#endif

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder(32);
            sb.Append(this.GetType().Name);
            sb.Append(" [");

            sb.Append("A=");
            sb.Append(this.A);
            sb.Append(", R=");
            sb.Append(this.R);
            sb.Append(", G=");
            sb.Append(this.G);
            sb.Append(", B=");
            sb.Append(this.B);

            sb.Append("]");

            return sb.ToString();
        }
    }
}