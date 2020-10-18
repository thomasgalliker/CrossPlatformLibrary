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
        [InlineData(null, null, null)]
        [InlineData("", null, "")]
        [InlineData("test", null, "test")]
        public void ShouldAddOrUpdateValue_String(string inputValue, string defaultValue, string expectedOutputValue)
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            var key = "testKey";

            // Act
            settingsSerivce.AddOrUpdateValue(key, inputValue);
            var outputValue = settingsSerivce.GetValueOrDefault(key, defaultValue);

            // Assert
            outputValue.Should().Be(expectedOutputValue);
        }

        [Theory]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        public void ShouldAddOrUpdateValue_Int(int inputValue, int defaultValue, int expectedOutputValue)
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            var key = "testKey";

            // Act
            settingsSerivce.AddOrUpdateValue(key, inputValue);
            var outputValue = settingsSerivce.GetValueOrDefault(key, defaultValue);

            // Assert
            outputValue.Should().Be(expectedOutputValue);
        }

        [Theory]
        [InlineData(null, null, null)]
        [InlineData(null, 0, null)]
        [InlineData(0, 0, 0)]
        [InlineData(1, 0, 1)]
        public void ShouldAddOrUpdateValue_NullableInt(int? inputValue, int? defaultValue, int? expectedOutputValue)
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            var key = "testKey";

            // Act
            settingsSerivce.AddOrUpdateValue(key, inputValue);
            var outputValue = settingsSerivce.GetValueOrDefault(key, defaultValue);

            // Assert
            outputValue.Should().Be(expectedOutputValue);
        }

        [Fact]
        public void ShouldGetValueOrDefault_NullableInt_NonExistingKey()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            settingsSerivce.RegisterDefaultConverter(new AppSettingsJsonConverter());

            var key = "testKey";

            // Act
            var outputValue = settingsSerivce.GetValueOrDefault<int?>(key);

            // Assert
            outputValue.Should().BeNull();
        }

        [Fact]
        public void ShouldAddOrUpdateValue_ReferenceType_ThrowsExceptionIfNoConverterAvailable()
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
        public void ShouldGetValueOrDefault_ReferenceType_ExistingKey()
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
        public void ShouldGetValueOrDefault_ReferenceType_NonExistingKey()
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

        [Fact]
        public void ShouldGetValueOrDefault_ValueType_ExistingKey()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            settingsSerivce.RegisterDefaultConverter(new AppSettingsJsonConverter());

            var key = "testKey";
            var inputValue = TimeSpan.FromSeconds(60);

            // Act
            settingsSerivce.AddOrUpdateValue(key, inputValue);
            var outputValue = settingsSerivce.GetValueOrDefault<TimeSpan>(key);

            // Assert
            outputValue.Should().Be(inputValue);
        }

        [Fact]
        public void ShouldGetValueOrDefault_ValueType_NonExistingKey()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            settingsSerivce.RegisterDefaultConverter(new AppSettingsJsonConverter());

            var key = "testKey";

            // Act
            var outputValue = settingsSerivce.GetValueOrDefault<TimeSpan>(key);

            // Assert
            outputValue.Should().Be(default(TimeSpan));
        }

        [Fact]
        public void ShouldGetValueOrDefault_Enum_NonExistingKey_WithDefaultValue()
        {
            // Arrange
            var settingsSerivce = new TestSettingsService(this.tracer);
            settingsSerivce.RegisterDefaultConverter(new AppSettingsJsonConverter());

            var key = "testKey";
            var defaultValue = MassUnit.KiloGrams;

            // Act
            var outputValue = settingsSerivce.GetValueOrDefault(key, defaultValue);

            // Assert
            outputValue.Should().Be(defaultValue);
        }
    }
}
