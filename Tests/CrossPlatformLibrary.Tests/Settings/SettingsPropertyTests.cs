using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using CrossPlatformLibrary.Settings;
using CrossPlatformLibrary.Utils;
using FluentAssertions;
using Moq;
using TestFactory.Utils;
using Xunit;
using Xunit.Abstractions;
using IConvertible = CrossPlatformLibrary.Settings.IConvertible;

namespace CrossPlatformLibrary.Tests.Settings
{
    public class SettingsPropertyTests
    {
        private readonly ITestOutputHelper testOutputHelper;

        public SettingsPropertyTests(ITestOutputHelper testOutputHelper)
        {
            this.testOutputHelper = testOutputHelper;
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfSettingsServiceIsNull()
        {
            // Arrange
            var settingsKeyName = "keyName";

            // Act
            Action action = () => new SettingsProperty<string>(null, settingsKeyName);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfKeyIsEmpty()
        {
            // Arrange
            var settingsKeyName = string.Empty;

            // Act
            Action action = () => new SettingsProperty<string>(null, settingsKeyName);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShouldNotAcceptKeyLengthLongerThan255Characters()
        {
            // Arrange
            var settingsServiceMock = new Mock<ISettingsService>();
            var settingsKeyName = RandomUtils.GenerateRandomString(256);

            // Act
            Action action = () => new SettingsProperty<string>(settingsServiceMock.Object, settingsKeyName);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void PerformanceTest_ShouldWriteProperty()
        {
            // Arrange
            const int writeOperations = 100;
            const int readOperationsPerWriteOperation = 10;
            var expectedDuration = TimeSpan.FromSeconds(1.2d);
            var settingsKeyName = RandomUtils.GenerateRandomString(255);

            var settingsServiceStub = new SettingsServiceStub();

            var settingsProperty = new SettingsProperty<string>(settingsServiceStub, settingsKeyName);

            // Act
            TimeSpan elapsed;
            using (var stopWatch = StopWatch.StartNew())
            {
                for (var i = 0; i < writeOperations; i++)
                {
                    settingsProperty.Value = $"Write attempt {i:D4}";

                    for (int j = 0; j < readOperationsPerWriteOperation; j++)
                    {
                        var value = settingsProperty.Value;
                        Debug.WriteLine(value);
                    }
                }

                elapsed = stopWatch.Elapsed;
            }

            // Assert
            this.testOutputHelper.WriteLine($"Finished with {nameof(writeOperations)}={writeOperations} and {nameof(readOperationsPerWriteOperation)}={readOperationsPerWriteOperation}  in {elapsed:g}s");
            elapsed.Should().BeLessThan(expectedDuration);

        }
    }

    public class SettingsServiceStub : ISettingsService
    {
        private readonly IDictionary<string, string> inMemoryStorage = new Dictionary<string, string>();

        public T GetValueOrDefault<T>(string key, T defaultValue = default(T))
        {
            Thread.Sleep(10);
            return (dynamic)this.inMemoryStorage[key];
        }

        public void AddOrUpdateValue<T>(string key, T value)
        {
            Thread.Sleep(10);
            this.inMemoryStorage[key] = value as string;
        }

        public void RegisterConverter<TSource, TTarget>(IConvertible convertible, bool reverse)
        {
            throw new NotImplementedException();
        }

        public void RegisterDefaultConverter(IConvertible convertible)
        {
            throw new NotImplementedException();
        }
    }
}