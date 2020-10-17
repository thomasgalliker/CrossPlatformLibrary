using System.Collections;
using CrossPlatformLibrary.Settings;
using FluentAssertions;
using Xunit;
using Xunit.Abstractions;
using CrossPlatformLibrary.Tests.Utils;
using CrossPlatformLibrary.Tests.Stubs;
using System;

namespace CrossPlatformLibrary.Tests.Settings.Internals
{
    public partial class SettingsServiceBaseTests
    {
        private readonly TestOutputHelperTracer tracer;

        public SettingsServiceBaseTests(ITestOutputHelper testOutputHelper)
        {
            this.tracer = new TestOutputHelperTracer(testOutputHelper, typeof(TestSettingsService));
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("test value")]
        public void ShouldAddOrUpdateValue_Strings(string inputValue)
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            var key = "testKey";

            // Act
            settingsSerivce.AddOrUpdateValue(key, inputValue);
            var outputValue = settingsSerivce.GetValueOrDefault<string>(key);

            // Assert
            outputValue.Should().Be(inputValue);
        }

        [Fact]
        public void ShouldAddOrUpdateValue_ComplexObject_ThrowsExceptionIfNoConverterAvailable()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);

            var key = "testKey";
            var inputValue = new Person
            {
                Name = "Test"
            };

            // Act
            Action action = () => settingsSerivce.AddOrUpdateValue(key, inputValue);

            // Assert
            action.Should().Throw<SettingsValueConversionException>().Which.Message.Should().Be("SerializeToString for sourceType=Person is currently not supported by the settings service");
        }

        [Fact]
        public void ShouldGetValueOrDefault_ComplexObject_UsingDefaultConverter_ExistingKey()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            settingsSerivce.RegisterDefaultConverter(new AppSettingsJsonConverter());

            var key = "testKey";
            var inputValue = new Person
            {
                Name = "Test"
            };

            // Act
            settingsSerivce.AddOrUpdateValue(key, inputValue);
            var outputValue = settingsSerivce.GetValueOrDefault<Person>(key);

            // Assert
            outputValue.Should().BeEquivalentTo(inputValue);
        }

        [Fact]
        public void ShouldGetValueOrDefault_ComplexObject_UsingDefaultConverter_NonExistingKey()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            settingsSerivce.RegisterDefaultConverter(new AppSettingsJsonConverter());

            var key = "testKey";

            // Act
            var outputValue = settingsSerivce.GetValueOrDefault<Person>(key);

            // Assert
            outputValue.Should().BeNull();
        }
    }
}
