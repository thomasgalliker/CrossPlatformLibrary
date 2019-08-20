using System;
using CrossPlatformLibrary.Extensions;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class TimeSpanExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ToSecondsStringTestData))]
        public void ShouldFormatTimeSpanUsingToSecondsString(TimeSpan timeSpan, string expectedOutput)
        {
            // Act
            var formattedTimeSpan = timeSpan.ToSecondsString();

            // Assert
            formattedTimeSpan.Should().Be(expectedOutput);
        }

        public class ToSecondsStringTestData : TheoryData<TimeSpan, string>
        {
            public ToSecondsStringTestData()
            {
                this.Add(new TimeSpan(), "0.0s");
                this.Add(new TimeSpan(0, 0, 0, 0, 1), "0.001s");
                this.Add(new TimeSpan(0, 0, 0, 0, 10), "0.01s");
                this.Add(new TimeSpan(0, 0, 0, 0, 100), "0.1s");
                this.Add(new TimeSpan(0, 0, 0, 1, 0), "1.0s");
                this.Add(new TimeSpan(0, 0, 1, 0, 0), "60.0s");
                this.Add(new TimeSpan(0, 0, 10, 0, 0), "600.0s");
                this.Add(TimeSpan.MaxValue, "922337203685.5s");
            }
        }

        [Theory]
        [ClassData(typeof(ToDurationStringTestData))]
        public void ShouldConvertTimeSpanToDurationString(TimeSpan timeSpan, string expectedDurationString)
        {
            // Act
            var durationString = timeSpan.ToDurationString();

            // Assert
            durationString.Should().Be(expectedDurationString);
        }

        public class ToDurationStringTestData : TheoryData<TimeSpan, string>
        {
            public ToDurationStringTestData()
            {
                // Special cases
                this.Add(TimeSpan.MinValue, "");
                this.Add(TimeSpan.MaxValue, "10675199d");
                this.Add(TimeSpan.Zero, "0s");

                // Simple TimeSpans
                this.Add(TimeSpan.FromDays(1), "1d");
                this.Add(TimeSpan.FromHours(1), "1h");
                this.Add(TimeSpan.FromMinutes(1), "1m");
                this.Add(TimeSpan.FromSeconds(1), "1s");
                this.Add(TimeSpan.FromMilliseconds(1), "0.001s");
                this.Add(TimeSpan.FromTicks(1L), "0.1µs");

                // Complex TimeSpans
                this.Add(new TimeSpan(1, 2, 3, 4), "1d");
                this.Add(new TimeSpan(788, 97, 66, 100, 1234), "792d");
            }
        }
    }
}