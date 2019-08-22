using System;
using FluentAssertions;
using InvoiceScanner.Extensions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Extensions
{
    public class GuidExtensionsTests
    {
        [Theory]
        [ClassData(typeof(IncrementTestData))]
        public void ShouldIncrementGuid(Guid input, Guid expectedOutput)
        {
            // Act
            var output = input.Increment();

            // Assert
            output.Should().Be(expectedOutput);
        }

        public class IncrementTestData : TheoryData<Guid, Guid>
        {
            public IncrementTestData()
            {
                this.Add(Guid.Empty, new Guid("00000000-0000-0000-0000-000000000001"));
                this.Add(new Guid("00000000-0000-0000-0000-000000000001"), new Guid("00000000-0000-0000-0000-000000000002"));
                this.Add(new Guid("00000000-0000-0000-0000-00000000000A"), new Guid("00000000-0000-0000-0000-00000000000B"));
                this.Add(new Guid("AAF9E39A-BD82-4A44-8E8F-516EC3795390"), new Guid("aaf9e39a-bd82-4a44-8e8f-516ec3795391"));
            }
        }
    }
}