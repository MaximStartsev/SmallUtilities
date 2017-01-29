using MaximStartsev.GamePadRemoteControl.XInput.Constants;
using System;

namespace MaximStartsev.GamepadRemoteControl.XBox.Events
{
    public sealed class ControllerButtonClickEventArgs : EventArgs
    {
        public ButtonFlags Button { get; private set; }
        public ControllerButtonClickEventArgs(ButtonFlags button)
        {
            Button = button;
        }
    }
}
