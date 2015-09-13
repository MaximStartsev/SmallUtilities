using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace WindowMovier
{
    /// <summary>
    /// Данный класс представляет собой открытое на одном из экранов окно
    /// </summary>
    internal class ScreenWindow
    {
        private Process _process;
        public String Title { get { return _process.MainWindowTitle; } }
        public String ProcessName { get { return _process.ProcessName; } }

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
            _process = process;
            _process.Exited += (sender,e)=> InvokeExited();
            _process.Disposed += (sender, e) => InvokeExited();
        }
        
        public void Move(Screen screen)
        { 
            var p = new Point();
            p.X = screen.WorkingArea.Left;
            p.Y = screen.WorkingArea.Top;
            var size = NativeMethods.GetWindowSize(_process.MainWindowHandle);
            var state = NativeMethods.GetWindowState(_process.MainWindowHandle);
            if (state==null)
            {
                InvokeExited();
            }
            NativeMethods.MoveWindow(_process.MainWindowHandle, p.X, p.Y, size.Width, size.Height, true);
            if (state == System.Windows.WindowState.Maximized)
            {
                NativeMethods.MaximizeWindow(_process.MainWindowHandle);
            }
        }

    }
}
