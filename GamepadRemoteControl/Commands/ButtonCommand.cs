using System;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    public abstract class ButtonCommand: Command
    {
        public abstract Action Action { get; }
    }
}
