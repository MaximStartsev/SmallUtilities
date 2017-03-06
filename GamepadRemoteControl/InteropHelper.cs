using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MaximStartsev.GamePadRemoteController
{
    internal static class InteropHelper
    {
        #region mouse click
        //Клик мыши
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        private static extern void mouse_event(UInt32 dwFlags, UInt32 dx, UInt32 dy, UInt32 cButtons, UInt32 dwExtraInfo);
        //Mouse actions
        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        private const int MOUSEEVENTF_RIGHTDOWN = 0x08;
        private const int MOUSEEVENTF_RIGHTUP = 0x10;
        public static void MouseLeftClick()
        {
            var X = (uint)Cursor.Position.X;
            var Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X, Y, 0, 0);
        }
        public static void MouseRightClick()
        {
            var X = (uint)Cursor.Position.X;
            var Y = (uint)Cursor.Position.Y;
            mouse_event(MOUSEEVENTF_RIGHTDOWN | MOUSEEVENTF_RIGHTUP, X, Y, 0, 0);
        }
        #endregion
        [DllImport("user32.dll")]
        private static extern void keybd_event(Byte bVk, Byte bScan, UInt32 dwFlags, Int32 dwExtraInfo);
        public static void PushKeyboard(ConsoleKey key)
        {
            keybd_event((byte)key, 0, 0, 0);
        }
        #region move window methods
        //Текущий активный процесс
        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowThreadProcessId(IntPtr hWnd, out uint ProcessId);
        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        public static Process GetActiveProcess()
        {
            IntPtr hwnd = GetForegroundWindow();
            uint pid;
            GetWindowThreadProcessId(hwnd, out pid);
            return Process.GetProcessById((int)pid);
        }
        //Размеры и положение окна
        [DllImport("user32.dll")]
        private static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        public static Point GetWindowPosition(IntPtr hWnd)
        {
            Rect rect = new Rect();
            GetWindowRect(hWnd, ref rect);
            return new Point()
            {
                X = rect.Left,
                Y = rect.Top
            };
        }
        public static Size GetWindowSize(IntPtr hWnd)
        {
            Rect rect = new Rect();
            Size size = new Size();
            GetWindowRect(hWnd, ref rect);
            size.Width = rect.Right - rect.Left;
            size.Height = rect.Bottom - rect.Top;
            return size;
        }
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        public static void MoveWindow(Process process, Screen screen)
        {
            var p = new Point();
            p.X = screen.WorkingArea.Left;
            p.Y = screen.WorkingArea.Top;
            var size = GetWindowSize(process.MainWindowHandle);
            MoveWindow(process.MainWindowHandle, p.X, p.Y, size.Width, size.Height, true);
        }
        #endregion
    }
    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }
}
