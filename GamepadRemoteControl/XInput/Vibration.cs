//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System.Runtime.InteropServices;
namespace MaximStartsev.GamePadRemoteControl.XInput
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vibration
    {
        [MarshalAs(UnmanagedType.I2)]
        public ushort LeftMotorSpeed;

        [MarshalAs(UnmanagedType.I2)]
        public ushort RightMotorSpeed;
    }
}
