using System;
using CrossPlatformLibrary.Forms.Behaviors;
using CrossPlatformLibrary.Forms.Converters;
using CrossPlatformLibrary.Forms.Tests.Testdata;
using FluentAssertions;
using Xamarin.Forms;
using Xamarin.Forms.Mocks;
using Xunit;

namespace CrossPlatformLibrary.Forms.Tests.Behaviors
{
    public partial class EventToCommandBehaviorTests
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
        public void ShouldSetBindingContext()
        {
            // Arrange
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithoutArgs),
            };
            var listView = new TestListView();
            listView.Behaviors.Add(behavior);

            // Act
            listView.BindingContext = new TestListViewModel();

            // Assert
            behavior.BindingContext.Should().BeOfType<TestListViewModel>();
        }

        [Fact]
        public void ShouldSetBindingContextNull_DeregisterEvent()
        {
            // Arrange
            var executedCommand = false;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<TestEventArgs>((e) =>
                {
                    executedCommand = true;
                })
            };
            var listView = new TestListView<TestEventArgs>();
            listView.Behaviors.Add(behavior);
            listView.BindingContext = new TestListViewModel();

            var eventArgs = new TestEventArgs();

            // Act
            listView.BindingContext = null;
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            executedCommand.Should().BeFalse();
            behavior.BindingContext.Should().BeNull();
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
            listView.BindingContext = new TestListViewModel();

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
                Command = new Command<TestEventArgs>((e) =>
                {
                    executedCommand = true;
                })
            };
            var listView = new TestListView<TestEventArgs>();
            listView.Behaviors.Add(behavior);
            listView.BindingContext = new TestListViewModel();

            var eventArgs = new TestEventArgs();

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
            listView.BindingContext = new TestListViewModel();

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
            listView.BindingContext = new TestListViewModel();

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
            listView.BindingContext = new TestListViewModel();

            // Act
            listView.RaiseEventWithoutArgs();

            // Assert
            executedCommand.Should().BeFalse();
        }

        [Fact]
        public void ShouldExecuteCommand_EventArgs_WithoutConverter()
        {
            // Arrange
            object returnedCommandParameter = null;
            var behavior = new EventToCommandBehavior
            {
                EventName = nameof(TestListView.EventWithEventArgs),
                Command = new Command<object>((e) =>
                {
                    returnedCommandParameter = e;
                }),
                CommandParameter = null,
                Converter = null,
            };
            var listView = new TestListView<TestEventArgs>();
            listView.Behaviors.Add(behavior);
            listView.BindingContext = new TestListViewModel();

            var eventArgs = new TestEventArgs
            {
                Param = "Test"
            };

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().BeEquivalentTo(eventArgs);
        }

        [Fact]
        public void ShouldExecuteCommand_EventArgs_WithConverter()
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
                CommandParameter = null,
                Converter = new TestEventArgsConverter(),
            };
            var listView = new TestListView<TestEventArgs>();
            listView.Behaviors.Add(behavior);
            listView.BindingContext = new TestListViewModel();

            var eventArgs = new TestEventArgs
            {
                Param = "Test"
            };

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().BeEquivalentTo(eventArgs.Param);
        }

        [Fact]
        public void ShouldExecuteCommand_CommandParameter_WithoutConverter()
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
                //Converter = new TestEventArgsConverter(),
            };
            var listView = new TestListView<TestEventArgs>();
            listView.Behaviors.Add(behavior);
            listView.BindingContext = new TestListViewModel();

            var eventArgs = new TestEventArgs
            {
                Param = "Test"
            };

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().BeEquivalentTo("CommandParameterValue");
        }

        [Fact]
        public void ShouldExecuteCommand_CommandParameter_WithConverter()
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
                Converter = new DebugConverter(),
            };
            var listView = new TestListView<TestEventArgs>();
            listView.Behaviors.Add(behavior);
            listView.BindingContext = new TestListViewModel();

            var eventArgs = new TestEventArgs
            {
                Param = "Test"
            };

            // Act
            listView.RaiseEventWithEventArgs(eventArgs);

            // Assert
            returnedCommandParameter.Should().BeEquivalentTo("CommandParameterValue");
        }
    }
}
