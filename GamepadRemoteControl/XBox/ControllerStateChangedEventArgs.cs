//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using MaximStartsev.GamePadRemoteControl.XInput;
using System;

namespace MaximStartsev.GamePadRemoteControl.XBox
{
    public sealed class ControllerStateChangedEventArgs : EventArgs
    {
        public State CurrentInputState { get; set; }
        public State PreviousInputState { get; set; }
    }
}
