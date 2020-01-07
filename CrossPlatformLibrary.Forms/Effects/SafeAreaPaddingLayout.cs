using System;
using System.Diagnostics;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CrossPlatformLibrary.Forms.Effects
{
    [DebuggerDisplay("SafeAreaPaddingLayout: {this.ToString()}")]
    [TypeConverter(typeof(SafeAreaPaddingLayoutConverter))]
    public sealed class SafeAreaPaddingLayout
    {
        private static readonly PaddingPosition[] All = Enum.GetValues(typeof(PaddingPosition)).OfType<SafeAreaPaddingLayout.PaddingPosition>().ToArray();

        public SafeAreaPaddingLayout(params PaddingPosition[] positions)
        {
            this.Positions = positions;
        }

        public PaddingPosition[] Positions { get; }

        public override string ToString()
        {
            return string.Format($"Positions = {{{string.Join(",", this.Positions.Select(p => $"{p}"))}}}");
        }

        public enum PaddingPosition
        {
            Left,
            Top,
            Right,
            Bottom,
        }

        public Thickness Transform(Thickness padding)
        {
            var notPresentPositions = All.Except(this.Positions);
            foreach (var paddingPosition in notPresentPositions)
            {
                if (paddingPosition == SafeAreaPaddingLayout.PaddingPosition.Left)
                {
                    padding.Left = 0;
                }
                else if (paddingPosition == SafeAreaPaddingLayout.PaddingPosition.Top)
                {
                    padding.Top = 0;
                }
                else if (paddingPosition == SafeAreaPaddingLayout.PaddingPosition.Right)
                {
                    padding.Right = 0;
                }
                else if (paddingPosition == SafeAreaPaddingLayout.PaddingPosition.Bottom)
                {
                    padding.Bottom = 0;
                }
            }

            return padding;
        }
    }

    [TypeConversion(typeof(SafeAreaPaddingLayout))]
    public sealed class SafeAreaPaddingLayoutConverter : TypeConverter
    {
        public override object ConvertFromInvariantString(string value)
        {
            if (value == null)
            {
                throw new InvalidOperationException(string.Format("Cannot convert null into {0}", typeof(SafeAreaPaddingLayout)));
            }

            var strArray = value.Split(new char[1] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            if (strArray.Length < 1 || strArray.Length > 4)
            {
                throw new InvalidOperationException(string.Format("Cannot convert \"{0}\" into {1}", value, typeof(SafeAreaPaddingLayout)));
            }

            var positions = strArray.Select(s => (SafeAreaPaddingLayout.PaddingPosition)Enum.Parse(typeof(SafeAreaPaddingLayout.PaddingPosition), s, true)).ToArray();

            return new SafeAreaPaddingLayout(positions);
        }
    }
}