using System;
using System.Globalization;
using CrossPlatformLibrary.Forms.Behaviors;
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
            Assert.Throws<ArgumentException>(action);
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
            Assert.True(executedCommand);
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
            Assert.True(executedCommand);
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
            Assert.Equal(eventArgs, returnedCommandParameter);
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
            Assert.False(executedCommand);
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
            Assert.False(executedCommand);
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
            Assert.Equal(eventArgs.Param, returnedCommandParameter);
        }

        [Fact]
        public void ShouldExecuteCommand_WithConverterAndCommandParameter()
        {
            // Arrange
            string returnedCommandParameter = null;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<string>(execute: (e) =>
                {
                    returnedCommandParameter = e;
                }, (e) => e == "Test"),
                CommandParameter = true,
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
            Assert.Equal(eventArgs.Param, returnedCommandParameter);
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
