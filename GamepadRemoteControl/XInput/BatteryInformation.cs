//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using MaximStartsev.GamePadRemoteControl.XInput.Constants;
using System.Runtime.InteropServices;

namespace MaximStartsev.GamePadRemoteControl.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct BatteryInformation
    {
        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(0)]
        public byte BatteryType;

        [MarshalAs(UnmanagedType.I1)]
        [FieldOffset(1)]
        public byte BatteryLevel;

        public override string ToString()
        {
            return string.Format("{0} {1}", (BatteryTypes)BatteryType, (BatteryLevel)BatteryLevel);
        }
    }
}
