﻿using System.Windows.Input;

namespace CrossPlatformLibrary.Forms.Controls
{
    public interface IDrilldownView
    {
        string Title { get; set; }

        bool IsEnabled { get; set; }

        ICommand Command { get; }

        object CommandParameter { get; }
    }
}