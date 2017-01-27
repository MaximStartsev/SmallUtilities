//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System.Runtime.InteropServices;

namespace MaximStartsev.GamePadRemoteControl.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct Keystroke
    {
        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(0)]
        public short VirtualKey;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(2)]
        public char Unicode;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(4)]
        public short Flags;

        [MarshalAs(UnmanagedType.I2)]
        [FieldOffset(5)]
        public byte UserIndex;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(6)]
        public byte HidCode;
    }
}
