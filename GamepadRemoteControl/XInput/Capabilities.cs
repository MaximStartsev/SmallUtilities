//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System.Runtime.InteropServices;

namespace MaximStartsev.GamePadRemoteControl.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Capabilities
    {
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(0)]
        byte Type;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(1)]
        public byte SubType;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(2)]
        public short Flags;


        [FieldOffset(4)]
        public Gamepad Gamepad;

        [FieldOffset(16)]
        public Vibration Vibration;
    }
}
