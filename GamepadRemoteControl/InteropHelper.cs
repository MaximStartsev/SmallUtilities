using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MaximStartsev.GamePadRemoteController
{
    internal static class InteropHelper
    {
        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);
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
