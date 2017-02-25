using MaximStartsev.GamePadRemoteControl.XInput;
using System;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    public abstract class StickCommand:Command
    {
        public abstract Action<Point> Action { get; }
    }
}
