//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
namespace MaximStartsev.GamePadRemoteControl.XInput.Constants
{
    public enum BatteryTypes : byte
    {
        BATTERY_TYPE_DISCONNECTED = 0x00,
        BATTERY_TYPE_WIRED = 0x01,
        BATTERY_TYPE_ALKALINE = 0x02,
        BATTERY_TYPE_NIMH = 0x03,
        BATTERY_TYPE_UNKNOWN = 0xFF,
    };
}
