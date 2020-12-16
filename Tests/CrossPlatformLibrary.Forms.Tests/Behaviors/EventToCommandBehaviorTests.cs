using System;
using System.Globalization;
using CrossPlatformLibrary.Forms.Behaviors;
using FluentAssertions;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Behaviors
{
    public class EventToCommandBehaviorTests
    {
        public EventToCommandBehaviorTests()
        {
            MockForms.Init();
        }

        [Fact]
        public void ShouldThrowExceptionIfEventDoesNotExist()
        {
            // Arrange
            var behavior = new EventToCommandBehavior
            {
                EventName = "NotExistingEvent"
            };
            var listView = new TestListView<object>();

            // Act
            Action action = () => listView.Behaviors.Add(behavior);

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void ShouldExecuteCommand_IfEventWithoutEventArgsIsRaised()
        {
            // Arrange
            var executedCommand = false;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithoutArgs),
                Command = new Command(() =>
                {
                    executedCommand = true;
                })
            };
            var listView = new TestListView();
            listView.Behaviors.Add(behavior);

            // Act
            listView.RaiseEventWithoutArgs();

            // Assert
            executedCommand.Should().BeTrue();
        }

        [Fact]
        public void ShouldExecuteCommand_IfEventWithEventArgsIsRaised_ReferenceType()
        {
            // Arrange
            var executedCommand = false;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<ProcessEventArgs>((e) =>
                {
                    executedCommand = true;
                })
            };
            var listView = new TestListView<ProcessEventArgs>();
            listView.Behaviors.Add(behavior);

            var eventArgs = new ProcessEventArgs();

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            executedCommand.Should().BeTrue();
        }

        [Fact]
        public void ShouldExecuteCommand_IfEventWithEventArgsIsRaised_ValueType()
        {
            // Arrange
            int? returnedCommandParameter = null;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<int>((e) =>
                {
                    returnedCommandParameter = e;
                })
            };
            var listView = new TestListView();
            listView.Behaviors.Add(behavior);

            var eventArgs = 99;

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().Be(eventArgs);
        }

        [Fact]
        public void ShouldNotExecuteCommand_IfCommandIsNull()
        {
            // Arrange
            var executedCommand = false;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithoutArgs),
                Command = new Command(() => executedCommand = true)
            };
            var listView = new TestListView();
            listView.Behaviors.Add(behavior);

            behavior.Command = null;

            // Act
            listView.RaiseEventWithoutArgs();

            // Assert
            executedCommand.Should().BeFalse();
        }

        [Fact]
        public void ShouldNotExecuteCommand_IfCanExecuteIsFalse()
        {
            // Arrange
            var executedCommand = false;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithoutArgs),
                Command = new Command(() => executedCommand = true, () => false)
            };
            var listView = new TestListView();
            listView.Behaviors.Add(behavior);

            // Act
            listView.RaiseEventWithoutArgs();

            // Assert
            executedCommand.Should().BeFalse();
        }

        [Fact]
        public void ShouldExecuteCommand_WithConverter()
        {
            // Arrange
            string returnedCommandParameter = null;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<string>((e) =>
                {
                    returnedCommandParameter = e;
                }),
                Converter = new ProcessEventArgsConverter(),
            };
            var listView = new TestListView<ProcessEventArgs>();
            listView.Behaviors.Add(behavior);

            var eventArgs = new ProcessEventArgs
            {
                Param = "Test"
            };

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().BeEquivalentTo(eventArgs.Param);
        }

        [Fact]
        public void ShouldExecuteCommand_WithCommandParameter()
        {
            // Arrange
            object returnedCommandParameter = null;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<object>(execute: (e) =>
                {
                    returnedCommandParameter = e;
                }),
                CommandParameter = "CommandParameterValue",
                Converter = new ProcessEventArgsConverter(),
            };
            var listView = new TestListView<ProcessEventArgs>();
            listView.Behaviors.Add(behavior);

            var eventArgs = new ProcessEventArgs
            {
                Param = "Test"
            };

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().BeEquivalentTo("CommandParameterValue");
        }

        public class TestListView : TestListView<object>
        {
        }

        public class TestListView<T> : ListView
        {
            public virtual event EventHandler EventWithoutArgs;

            public void RaiseEventWithoutArgs()
            {
                EventWithoutArgs?.Invoke(this, EventArgs.Empty);
            }

            public event EventHandler<T> EventWithEventArgs;

            public void RaiseEventWithEventArgs(T eventArgs)
            {
                EventWithEventArgs?.Invoke(this, eventArgs);
            }
        }

        public class ProcessEventArgs : EventArgs
        {
            public string Param { get; set; }
        }

        private class ProcessEventArgsConverter : IValueConverter
        {
            public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
            {
                return (value as ProcessEventArgs)?.Param;
            }

            public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
            {
                throw new NotImplementedException();
            }
        }
    }
}
