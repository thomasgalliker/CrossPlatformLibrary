using System;
using System.Collections.Generic;
using System.Linq;
using CrossPlatformLibrary.Mvvm;
using FluentAssertions;
using Xunit;

namespace CrossPlatformLibrary.Tests.Mvvm
{
    public class BindableBaseTests
    {
        [Fact]
        public void ShouldRaisePropertyChanged()
        {
            // Arrange
            var viewModel = new TestViewModel();

            var propertyChangedCallbacks = new List<string>();
            viewModel.PropertyChanged += (sender, args) => { propertyChangedCallbacks.Add(args.PropertyName); };

            // Act
            viewModel.UserName = "username";

            // Assert
            propertyChangedCallbacks.Should().HaveCount(2);
            propertyChangedCallbacks.ElementAt(0).Should().Be("UserName");
            propertyChangedCallbacks.ElementAt(1).Should().Be("DependingOnUserName");
        }

        [Fact]
        public void ShouldNotRaisePropertyChangedIfEqual()
        {
            // Arrange
            var viewModel = new TestViewModel();
            viewModel.UserName = "username";

            var propertyChangedCallbacks = new List<string>();
            viewModel.PropertyChanged += (sender, args) => { propertyChangedCallbacks.Add(args.PropertyName); };

            // Act
            viewModel.UserName = "username";

            // Assert
            propertyChangedCallbacks.Should().HaveCount(0);
        }

        [Fact]
        public void ShouldRaisePropertyChanged_InReferencedObject()
        {
            // Arrange
            var viewModel = new TestViewModel();

            var propertyChangedCallbacks = new List<string>();
            viewModel.PropertyChanged += (sender, args) => { propertyChangedCallbacks.Add(args.PropertyName); };

            // Act
            viewModel.UserName2 = "username2";

            // Assert
            propertyChangedCallbacks.Should().HaveCount(1);
            propertyChangedCallbacks.ElementAt(0).Should().Be("UserName2");
        }

        [Fact]
        public void ShouldThrowExceptionIfPropertyNameDoesNotExistInSourceType()
        {
            // Arrange
            var viewModel = new TestViewModel();

            // Act
            Action action = () => viewModel.WrongProperty = "wrong";

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        public class TestViewModel : BindableBase
        {
            private string userName;
            private string wrongProperty;
            private User user = new User();

            public string UserName
            {
                get => this.userName;
                set
                {
                    if (this.SetProperty(ref this.userName, value))
                    {
                        this.RaisePropertyChanged("DependingOnUserName");
                    }
                }
            }

            public string UserName2
            {
                get => this.user?.UserName2;
                set => this.SetProperty(this.user, value);
            }

            public string WrongProperty
            {
                get => this.wrongProperty;
                set => this.SetProperty(this.wrongProperty, value);
            }

            private class User
            {
                public string UserName1 { get; set; }

                public string UserName2 { get; set; }
            }
        }
    }
}
