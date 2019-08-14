using System;
using System.Collections.Generic;
using System.Threading;
using CrossPlatformLibrary.Forms.Extensions;
using FluentAssertions;
using SampleApp.Model;
using Xamarin.Forms;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Extensions
{
    public class MessagingCenterExtensionsTests
    {
        [Fact]
        public void ShouldSubscribeAndSend_WithoutArguments()
        {
            // Arrange
            var messagingCenter = MessagingCenter.Instance;
            var message = GenerateMessage();
            var received = 0;

            // Act
            messagingCenter.Subscribe(message, () => { Interlocked.Increment(ref received); });
            messagingCenter.Send(message);

            // Assert
            received.Should().Be(1, because: "send has been called once");
        }

        [Fact]
        public void ShouldUnsubscribe_WithoutArguments()
        {
            // Arrange
            var messagingCenter = MessagingCenter.Instance;
            var message = GenerateMessage();
            var received = 0;

            // Act
            var subscription = messagingCenter.Subscribe(message, () => { Interlocked.Increment(ref received); });
            messagingCenter.Send(message);
            subscription.Unsubscribe();

            messagingCenter.Send(message);
            messagingCenter.Send(message);
            messagingCenter.Send(message);

            // Assert
            received.Should().Be(1, because: "unsubscribe happened after the first send");
        }

        [Fact]
        public void ShouldSubscribeAndSend_WithArguments()
        {
            // Arrange
            var messagingCenter = MessagingCenter.Instance;
            var message = GenerateMessage();
            var received = new List<PersonDto>();
            var personDto = new PersonDto { UserName = "test" };

            // Act
            messagingCenter.Subscribe<PersonDto>(message, p => { received.Add(p); });
            messagingCenter.Send(message, personDto);
            messagingCenter.Send(message, personDto);

            // Assert
            received.Should().HaveCount(2, because: "send has been called twice");
            received.Should().Contain(new[]
            {
                personDto,
                personDto
            });
        }

        [Fact]
        public void ShouldUnsubscribe_WithArguments()
        {
            // Arrange
            var messagingCenter = MessagingCenter.Instance;
            var message = GenerateMessage();
            var received = new List<PersonDto>();
            var personDto = new PersonDto { UserName = "test" };

            // Act
            var subscription = messagingCenter.Subscribe<PersonDto>(message, p => { received.Add(p); });
            messagingCenter.Send(message, personDto);
            subscription.Unsubscribe();

            messagingCenter.Send(message);
            messagingCenter.Send(message);
            messagingCenter.Send(message);

            // Assert
            received.Should().HaveCount(1, because: "unsubscribe happened after the first send");
        }

        private static string GenerateMessage()
        {
            return $"Message_{Guid.NewGuid().ToString("N").Substring(0, 5).ToUpperInvariant()}";
        }
    }
}
