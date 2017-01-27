//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System.Runtime.InteropServices;

namespace MaximStartsev.GamePadRemoteControl.XInput
{
    [StructLayout(LayoutKind.Explicit)]
    public struct State
    {
        [FieldOffset(0)]
        public int PacketNumber;

        [FieldOffset(4)]
        public Gamepad Gamepad;

        public void Copy(State source)
        {
            PacketNumber = source.PacketNumber;
            Gamepad.Copy(source.Gamepad);
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) || (!(obj is State)))
                return false;
            State source = (State)obj;

            return ((PacketNumber == source.PacketNumber)
                && (Gamepad.Equals(source.Gamepad)));
        }
    }
}
