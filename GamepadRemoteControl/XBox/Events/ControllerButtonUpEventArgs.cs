using MaximStartsev.GamePadRemoteControl.XInput.Constants;
using System;

namespace MaximStartsev.GamepadRemoteControl.XBox.Events
{
    public sealed class ControllerButtonUpEventArgs : EventArgs
    {
        public bool Handled { get; set; }
        public ButtonFlags Button { get; private set; }
        public ControllerButtonUpEventArgs(ButtonFlags button)
        {
            Button = button;
            Handled = false;
        }
    }
}