using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace MaximStartsev.SmallUtilities.WindowMovier
{
    /// <summary>
    /// Данный класс представляет собой открытое на одном из экранов окно
    /// </summary>
    internal class ScreenWindow
    {
        public String Title { get; private set; }
        private IntPtr _handle;

        public override string ToString()
        {
            return Title;
        }
        public event EventHandler Exited;
        private void InvokeExited()
        {
            if (Exited!=null)
            {
                Exited(this, new EventArgs());
            }
        }

        public ScreenWindow(Process process)
        {
            Title = process.MainWindowTitle;
            _handle = process.MainWindowHandle;
        }
        public ScreenWindow(IntPtr handle, String title)
        {
            _handle = handle;
            Title = title;
        }

        public void Move(Screen screen)
        {
            var p = new Point();
            p.X = screen.WorkingArea.Left;
            p.Y = screen.WorkingArea.Top;
            var size = NativeMethods.GetWindowSize(_handle);
            var state = NativeMethods.GetWindowState(_handle);
            if (state==null)
            {
                InvokeExited();
            }
            NativeMethods.MoveWindow(_handle, p.X, p.Y, size.Width, size.Height, true);
            if (state == System.Windows.WindowState.Maximized)
            {
                NativeMethods.MaximizeWindow(_handle);
            }
        }

    }
}
