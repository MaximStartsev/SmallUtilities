using MaximStartsev.SmallUtilities.SearchJobCRM.Core;
using MaximStartsev.SmallUtilities.SearchJobCRM.Utilities;
using MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels;
using System;
using System.Windows;

namespace MaximStartsev.SmallUtilities.SearchJobCRM
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MVVMCore.MainWindow = this;
            DataContext = new MainViewModel();
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = (DataContext as MainViewModel).Close();

        }
    }
}
