//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
namespace MaximStartsev.GamePadRemoteControl.XInput.Constants
{
    public static class Constants
    {
        public const int XINPUT_DEVTYPE_GAMEPAD = 0x01;
        // Device subtypes available in XINPUT_CAPABILITIES
        public const int XINPUT_DEVSUBTYPE_GAMEPAD = 0x01;
        //Gamepad thresholds
        public const int XINPUT_GAMEPAD_LEFT_THUMB_DEADZONE = 7849;
        public const int XINPUT_GAMEPAD_RIGHT_THUMB_DEADZONE = 8689;
        public const int XINPUT_GAMEPAD_TRIGGER_THRESHOLD = 30;
        //lags to pass to XInputGetCapabilities
        public const int XINPUT_FLAG_GAMEPAD = 0x00000001;
    }
}
