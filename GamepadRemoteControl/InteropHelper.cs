using System;
using System.Runtime.InteropServices;

namespace MaximStartsev.GamePadRemoteController
{
    internal static class InteropHelper
    {
        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
        public static void PushKeyboard(ConsoleKey key)
        {
            keybd_event((byte)key, 0, 0, 0);
        }
    }
}
