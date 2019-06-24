﻿using System.Windows.Input;
using Xamarin.Forms;

namespace CrossPlatformLibrary.Forms.Controls
{
    public class DrilldownView : BindableObject
    {
        public static readonly BindableProperty TitleProperty =
            BindableProperty.Create(
                nameof(Title),
                typeof(string),
                typeof(DrilldownView));

        public string Title
        {
            get { return (string)this.GetValue(TitleProperty); }
            set { this.SetValue(TitleProperty, value); }
        }

        public static readonly BindableProperty CommandProperty =
            BindableProperty.Create(
                nameof(Command),
                typeof(ICommand),
                typeof(DrilldownView));

        public ICommand Command
        {
            get { return (ICommand)this.GetValue(CommandProperty); }
            set { this.SetValue(CommandProperty, value); }
        }

        public static readonly BindableProperty CommandParameterProperty =
            BindableProperty.Create(
                nameof(CommandParameter),
                typeof(object),
                typeof(DrilldownView),
                null,
                BindingMode.OneWay);

        public object CommandParameter
        {
            get => this.GetValue(CommandParameterProperty);
            set => this.SetValue(CommandParameterProperty, value);
        }

        public static readonly BindableProperty IsEnabledProperty =
            BindableProperty.Create(
                nameof(IsEnabled),
                typeof(bool),
                typeof(DrilldownView),
                true,
                BindingMode.OneWay);

        public bool IsEnabled
        {
            get { return (bool)this.GetValue(IsEnabledProperty); }
            set { this.SetValue(IsEnabledProperty, value); }
        }
    }
}