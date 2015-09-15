
using CrossPlatformLibrary.IoC;
using CrossPlatformLibrary.Tests.Stubs;

using FluentAssertions;

using Xunit;

namespace CrossPlatformLibrary.Tests.IoC
{
    [Collection("IoC")]
    public class ResolvedParameterTests
    {
        public ResolvedParameterTests()
        {
            SimpleIoc.Default.Reset();
        }

        [Fact]
        public void TestResolvedParameter()
        {
            // Arrange
            var instanceName = "namedInstance";
            var testObject1 = new TestClass1();
            SimpleIoc.Default.Register<ITestClass1>(() => testObject1, instanceName);
            SimpleIoc.Default.Register<ITestClass10, TestClass10>(new ResolvedParameter<ITestClass1>(instanceName));

            // Act
            var testObject10 = SimpleIoc.Default.GetInstance<ITestClass10>();

            // Assert
            testObject10.Should().NotBeNull();
            testObject10.TestClass1.Should().NotBeNull();
            testObject10.TestClass1.Should().BeOfType<TestClass1>();
            testObject10.TestClass1.As<TestClass1>().Should().Be(testObject1);
            SimpleIoc.Default.IsRegistered<ITestClass1>().Should().BeTrue();
            SimpleIoc.Default.IsRegistered<ITestClass10>().Should().BeTrue();
            SimpleIoc.Default.ContainsCreated<ITestClass1>(instanceName).Should().BeTrue();
            SimpleIoc.Default.ContainsCreated<ITestClass10>().Should().BeTrue();
        }
    }
}
