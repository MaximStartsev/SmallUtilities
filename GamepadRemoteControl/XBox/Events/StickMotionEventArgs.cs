using MaximStartsev.GamePadRemoteControl.XInput;
using System;

namespace MaximStartsev.GamepadRemoteControl.XBox.Events
{
    public class StickMotionEventArgs: EventArgs
    {
        public Point Position { get; private set; }
        public StickMotionEventArgs(Point position)
        {
            Position = position;
        }
    }
}
