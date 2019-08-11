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
    }
}