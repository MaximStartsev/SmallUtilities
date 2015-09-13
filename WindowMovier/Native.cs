using System;
using System.Drawing;
using System.Runtime.InteropServices;
using WindowState = System.Windows.WindowState;

namespace WindowMovier
{
    /// <summary>
    /// Обёртка для функций winapi
    /// </summary>
    internal static class NativeMethods
    {
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern IntPtr FindWindow(string strClassName, string strWindowName);
        //Размеры и положение окна
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        public static Size GetWindowSize(IntPtr hWnd)
        {
            Rect rect = new Rect();
            Size size = new Size();
            GetWindowRect(hWnd, ref rect);
            size.Width = rect.Right - rect.Left;
            size.Height = rect.Bottom - rect.Top;
            return size;
        }
        //Перемещение окна
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);
        //Положение окна
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);
        private struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point ptMinPosition;
            public Point ptMaxPosition;
            public Rectangle rcNormalPosition;
        }
        public static WindowState? GetWindowState(IntPtr hWnd)
        {
            if (hWnd != IntPtr.Zero)
            {
                WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
                GetWindowPlacement(hWnd, ref placement);
                switch (placement.showCmd)
                {
                    case 1: return WindowState.Normal;
                    case 2: return WindowState.Minimized;
                    case 3: return WindowState.Maximized;
                    default: return null;
                }
            }
            else
            {
                throw new ArgumentException("Parameter hWnd can't be zero.");
            }
        }
        //Максимизация окна
        [DllImport("user32.dll")]
        private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
        public static void MaximizeWindow(IntPtr hWnd)
        {
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
                ShowWindow(hWnd, 3);
            }
            else
            {
                throw new ArgumentException("Parameter hWnd can't be zero.");
            }
        }

    }
}
