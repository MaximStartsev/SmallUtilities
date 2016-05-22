using System;
using System.Text;
using System.Windows;
using System.Windows.Forms;

namespace MaximStartsev.SmallUtilities.WindowMovier
{
    public struct Rect
    {
        public int Left { get; set; }
        public int Top { get; set; }
        public int Right { get; set; }
        public int Bottom { get; set; }
    }
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            foreach (var screen in Screen.AllScreens)
            {
                ScreensComboBox.Items.Add(new ScreenItem(screen));
            }
            ScreensComboBox.SelectedIndex = 0;
            UpdateWindowsList();
        }
        private void UpdateWindowsList()
        {
            WindowsListBox.Items.Clear();
            NativeMethods.EnumDelegate filter = delegate (IntPtr hWnd, int lParam)
            {
                StringBuilder strbTitle = new StringBuilder(255);
                int nLength = NativeMethods.GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
                string strTitle = strbTitle.ToString();

                if (NativeMethods.IsWindowVisible(hWnd) && !string.IsNullOrEmpty(strTitle) && strTitle!="Пуск" && strTitle!="Start")
                {
                    var screenWindow = new ScreenWindow(hWnd, strTitle);
                    WindowsListBox.Items.Add(screenWindow);
                }
                return true;
            };

            NativeMethods.EnumDesktopWindows(IntPtr.Zero, filter, IntPtr.Zero);
        }
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            var trayIcon = new TrayIcon(this);
        }
        private void ScreenWindow_Exited(object sender, EventArgs e)
        {
            var screenWindow = sender as ScreenWindow;
            screenWindow.Exited -= ScreenWindow_Exited;
            WindowsListBox.Items.Remove(screenWindow);
        }

        private void Click_Move(object sender, RoutedEventArgs e)
        {
            if (ScreensComboBox.SelectedItem!=null && WindowsListBox.SelectedItems.Count!=0)
            {
                var screen = (ScreensComboBox.SelectedItem as ScreenItem).Screen;
                foreach (ScreenWindow item in WindowsListBox.SelectedItems)
                {
                    item.Move(screen);
                }
            }
        }

        private void Click_Move_All(object sender, RoutedEventArgs e)
        {
            if (ScreensComboBox.SelectedItem != null && WindowsListBox.Items.Count != 0)
            {
                var screen = (ScreensComboBox.SelectedItem as ScreenItem).Screen;
                foreach (ScreenWindow item in WindowsListBox.Items)
                {
                    item.Move(screen);
                }
            }
        }

        private void Click_Refresh(object sender, RoutedEventArgs e)
        {
            UpdateWindowsList();
        }
    }
}
