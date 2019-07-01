using System;
using CrossPlatformLibrary.Settings;
using CrossPlatformLibrary.Utils;
using FluentAssertions;
using Moq;
using Xunit;

namespace CrossPlatformLibrary.Tests.Settings
{
    public class SettingsPropertyTests
    {
        [Fact]
        public void ShouldThrowArgumentNullExceptionIfSettingsServiceIsNull()
        {
            // Arrange
            string settingsKeyName = "keyName";

            // Act
            Action action = () => new SettingsProperty<string>(null, settingsKeyName);

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ShouldThrowArgumentNullExceptionIfKeyIsEmpty()
        {
            // Arrange
            string settingsKeyName = string.Empty;

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
            string settingsKeyName = RandomUtils.GenerateRandomString(256);

            // Act
            Action action = () => new SettingsProperty<string>(settingsServiceMock.Object, settingsKeyName);

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}