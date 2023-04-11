using System;
using CrossPlatformLibrary.Extensions;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class StringExtensionsTests
    {
        [Fact]
        public void ShouldCheckIfStringContains_ReturnsTrue_InvariantCultureIgnoreCase()
        {
            // Arrange
            var inputString = "ABCDefg";

            // Act
            var contains = StringExtensions.Contains(inputString, "cde", StringComparison.InvariantCultureIgnoreCase);

            // Assert
            contains.Should().BeTrue();
        }

        [Fact]
        public void ShouldCheckIfStringContains_ReturnsFalse_InvariantCultureIgnoreCase()
        {
            // Arrange
            var inputString = "ABCDefg";

            // Act
            var contains = StringExtensions.Contains(inputString, "xxx", StringComparison.InvariantCultureIgnoreCase);

            // Assert
            contains.Should().BeFalse();
        }

        [Fact]
        public void ShouldCheckIfStringContainsAny_ReturnsTrue()
        {
            // Arrange
            var inputString = "ABCDefg";

            // Act
            var contains = inputString.ContainsAny(new []{ "CDe", "xxx"});

            // Assert
            contains.Should().BeTrue();
        }

        [Fact]
        public void ShouldCheckIfStringContainsAny_ReturnsFalse()
        {
            // Arrange
            var inputString = "ABCDefg";

            // Act
            var contains = inputString.ContainsAny(new []{ "cde", "xxx"});

            // Assert
            contains.Should().BeFalse();
        }
        
        [Fact]
        public void ShouldCheckIfStringContainsAny_ReturnsTrue_InvariantCultureIgnoreCase()
        {
            // Arrange
            var inputString = "ABCDefg";

            // Act
            var contains = inputString.ContainsAny(new []{ "cde", "xxx"}, StringComparison.InvariantCultureIgnoreCase);

            // Assert
            contains.Should().BeTrue();
        }

        [Fact]
        public void ShouldCheckIfStringContainsAny_ReturnsFalse_InvariantCultureIgnoreCase()
        {
            // Arrange
            var inputString = "ABCDefg";

            // Act
            var contains = inputString.ContainsAny(new []{ "xxx", "yyy"}, StringComparison.InvariantCultureIgnoreCase);

            // Assert
            contains.Should().BeFalse();
        }

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
        [ClassData(typeof(TrimWhitespacesTestData))]
        public void ShouldTrimWhitespaces(string input, string expectedOutput)
        {
            // Act
            var output = input.TrimWhitespaces();

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class TrimWhitespacesTestData : TheoryData<string, string>
        {
            public TrimWhitespacesTestData()
            {
                this.Add($"", "");
                this.Add($" ", "");
                this.Add($"  ", "");
                this.Add($"test", "test");
                this.Add($"  A  and     B   ", "A and B");
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