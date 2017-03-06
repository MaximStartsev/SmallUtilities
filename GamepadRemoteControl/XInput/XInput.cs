//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using System.Runtime.InteropServices;

namespace MaximStartsev.GamePadRemoteControl.XInput
{
    internal static class XInput
    {
#if WINDOWS7
        [DllImport("xinput9_1_0.dll")]
        public static extern int XInputGetState
        (
            int dwUserIndex,  // [in] Index of the gamer associated with the device
            ref XInputState pState        // [out] Receives the current state
        );

        [DllImport("xinput9_1_0.dll")]
        public static extern int XInputSetState
        (
            int dwUserIndex,  // [in] Index of the gamer associated with the device
            ref XInputVibration pVibration    // [in, out] The vibration information to send to the controller
        );

        [DllImport("xinput9_1_0.dll")]
        public static extern int XInputGetCapabilities
        (
            int dwUserIndex,   // [in] Index of the gamer associated with the device
            int dwFlags,       // [in] Input flags that identify the device type
            ref XInputCapabilities pCapabilities  // [out] Receives the capabilities
        );


        //this function is not available prior to Windows 8
        public static int XInputGetBatteryInformation
        (
              int dwUserIndex,        // Index of the gamer associated with the device
              byte devType,            // Which device on this user index
            ref BatteryInformation pBatteryInformation // Contains the level and types of batteries
        )
        {
           return 0;
        }

        //this function is not available prior to Windows 8
        public static int XInputGetKeystroke
        (
            int dwUserIndex,              // Index of the gamer associated with the device
            int dwReserved,               // Reserved for future use
           ref      XInputKeystroke pKeystroke    // Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
        )
        {
            return 0;
        }
#else


        [DllImport("xinput1_4.dll")]
        internal static extern int XInputGetState
        (
            int dwUserIndex,  // [in] Index of the gamer associated with the device
            ref State pState        // [out] Receives the current state
        );

        [DllImport("xinput1_4.dll")]
        internal static extern int XInputSetState
        (
            int dwUserIndex,  // [in] Index of the gamer associated with the device
            ref Vibration pVibration    // [in, out] The vibration information to send to the controller
        );

        [DllImport("xinput1_4.dll")]
        internal static extern int XInputGetCapabilities
        (
            int dwUserIndex,   // [in] Index of the gamer associated with the device
            int dwFlags,       // [in] Input flags that identify the device type
            ref Capabilities pCapabilities  // [out] Receives the capabilities
        );


        [DllImport("xinput1_4.dll")]
        internal static extern int XInputGetBatteryInformation
        (
              int dwUserIndex,        // Index of the gamer associated with the device
              byte devType,            // Which device on this user index
            ref BatteryInformation pBatteryInformation // Contains the level and types of batteries
        );

        [DllImport("xinput1_4.dll")]
        internal static extern int XInputGetKeystroke
        (
            int dwUserIndex,              // Index of the gamer associated with the device
            int dwReserved,               // Reserved for future use
           ref Keystroke pKeystroke    // Pointer to an XINPUT_KEYSTROKE structure that receives an input event.
        );
#endif
    }
}
