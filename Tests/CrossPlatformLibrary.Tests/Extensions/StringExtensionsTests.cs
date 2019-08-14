using System;
using CrossPlatformLibrary.Extensions;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Theory]
        [ClassData(typeof(ToUpperFirstTestData))]
        public void ShouldToUpperFirst(string input, string expectedOutput)
        {
            // Act
            var output = input.ToUpperFirst();

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class ToUpperFirstTestData : TheoryData<string, string>
        {
            public ToUpperFirstTestData()
            {
                this.Add(null, null);
                this.Add("", "");
                this.Add("t", "T");
                this.Add("test", "Test");
            }
        }

        [Theory]
        [ClassData(typeof(TrimStartAndEndTestData))]
        public void ShouldTrimStartAndEnd(string input, string expectedOutput)
        {
            // Act
            var output = input.TrimStartAndEnd();

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class TrimStartAndEndTestData : TheoryData<string, string>
        {
            public TrimStartAndEndTestData()
            {
                this.Add($"{Environment.NewLine}", "");
                this.Add($"test", "test");
                this.Add($"{Environment.NewLine}test{Environment.NewLine}{Environment.NewLine}", "test");
                this.Add($"{Environment.NewLine}test{Environment.NewLine}test2{Environment.NewLine}", $"test{Environment.NewLine}test2");
            }
        }

        [Theory]
        [ClassData(typeof(RemoveEmptyLinesTestData))]
        public void ShouldRemoveEmptyLines(string input, string expectedOutput)
        {
            // Act
            var output = input.RemoveEmptyLines();

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class RemoveEmptyLinesTestData : TheoryData<string, string>
        {
            public RemoveEmptyLinesTestData()
            {
                this.Add($"{Environment.NewLine}", "");
                this.Add($"test", "test");
                this.Add($"{Environment.NewLine}test{Environment.NewLine}{Environment.NewLine}", "test");
            }
        }
    }
}