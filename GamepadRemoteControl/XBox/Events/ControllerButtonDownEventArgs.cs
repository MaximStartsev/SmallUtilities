using MaximStartsev.GamePadRemoteControl.XInput.Constants;
using System;

namespace MaximStartsev.GamepadRemoteControl.XBox.Events
{
    public sealed class ControllerButtonDownEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public ButtonFlags Button { get; private set; }
        public ControllerButtonDownEventArgs(ButtonFlags button)
        {
            Button = button;
            Handled = false;
        }
    }
}
