using System;
using System.Diagnostics;

namespace CrossPlatformLibrary.Forms.Controls
{
    [DebuggerDisplay("DateRange: {this.Start:u} - {this.End:u}")]
    public class DateRange
    {
        public static readonly DateRange MinMaxValue = new DateRange(DateTime.MinValue, DateTime.MaxValue);

        public DateRange(DateTime start, DateTime end)
        {
            this.Start = start;
            this.End = end;
        }

        public DateTime Start { get; }

        public DateTime End { get; }
    }
}