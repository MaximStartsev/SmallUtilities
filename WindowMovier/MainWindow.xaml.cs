using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Forms;

namespace WindowMovier
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
            Process[] processlist = Process.GetProcesses();

            foreach (Process process in processlist)
            {
                if (!String.IsNullOrEmpty(process.MainWindowTitle))
                {
                    var screenWindow = new ScreenWindow(process);
                    screenWindow.Exited += ScreenWindow_Exited;
                    WindowsListBox.Items.Add(screenWindow);
                }
            }
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
    }
}
