using System;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Interop;

namespace WindowMovier
{
    class TrayIcon:IDisposable
    {
        private WindowState _CurrentWindowState = WindowState.Normal;
        public WindowState CurrentWindowState
        {
            get { return _CurrentWindowState; }
            set { _CurrentWindowState = value; }
        }
        private NotifyIcon NotifyIcon = null;
        private MainWindow _mainWindow;
        public TrayIcon(MainWindow mainWindow)
        {
            _mainWindow = mainWindow;
            CreateTrayIcon();
        }
        private bool CreateTrayIcon()
        {
            bool result = false;
            if (NotifyIcon == null)
            { 
                NotifyIcon = new NotifyIcon(); 
                NotifyIcon.Icon = Resources.Resource1.Arrow; 
                NotifyIcon.Text = "Window Movier"; 
                NotifyIcon.Click += delegate (object sender, EventArgs e)
                {
                    if ((e as MouseEventArgs).Button == MouseButtons.Left)
                    {
                        ShowHideMainWindow(sender, null);
                    }
                };
                NotifyIcon.ContextMenu = GenerateContextMenu();
                result = true;
            }
            else
            { 
                result = true;
            }
            NotifyIcon.Visible = true;
            return result;
        }
        private ContextMenu GenerateContextMenu()
        {
            var contextMenu = new ContextMenu();
            foreach (ScreenItem item in _mainWindow.ScreensComboBox.Items)
            {
                contextMenu.MenuItems.Add(new MenuItem(String.Format("Открыть на {0}", item.Content),(o,e)=>
                {
                    _mainWindow.Show();
                    _mainWindow.WindowState = CurrentWindowState;
                    _mainWindow.Activate();
                    var p = new System.Drawing.Point();
                    p.X = item.Screen.WorkingArea.Left;
                    p.Y = item.Screen.WorkingArea.Top;
                    var handle = new WindowInteropHelper(_mainWindow).Handle;
                    var size = NativeMethods.GetWindowSize(handle);
                    var state = NativeMethods.GetWindowState(handle);
                    NativeMethods.MoveWindow(handle, p.X, p.Y, size.Width, size.Height, true);
                    if (state == WindowState.Maximized)
                    {
                        NativeMethods.MaximizeWindow(handle);
                    }
                }));
            }
            contextMenu.MenuItems.Add(new MenuItem("Закрыть", (ob, e) => _mainWindow.Close()));
            return contextMenu;
        }
        private void ShowHideMainWindow(object sender, RoutedEventArgs e)
        {
            if (_mainWindow.IsVisible)
            {
                _mainWindow.Hide();
            }
            else
            { 
                _mainWindow.Show();
                _mainWindow.WindowState = CurrentWindowState;
                _mainWindow.Activate();
            }
        }

        public void Dispose()
        {
            if (NotifyIcon!=null)
            {
                NotifyIcon.Dispose();
            }
        }
    }
}
