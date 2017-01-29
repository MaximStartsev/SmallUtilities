//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System;

namespace MaximStartsev.GamePadRemoteControl.XInput.Constants
{
    [Flags]
    public enum ButtonFlags : int
    {
        DpadUp = 0x0001,
        DpadDown = 0x0002,
        DPadLeft = 0x0004,
        DPadRight = 0x0008,
        Start = 0x0010,
        Select = 0x0020,
        LeftThumb = 0x0040,
        RightThumb = 0x0080,
        LeftShoulder = 0x0100,
        RightShoulder = 0x0200,
        A = 0x1000,
        B = 0x2000,
        X = 0x4000,
        Y = 0x8000,
    };
}
