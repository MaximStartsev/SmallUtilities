using System;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    internal interface ICommand
    {
        string Title { get; }
        Action Action { get; }
    }
}
