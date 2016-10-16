
using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels;
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
            ViewModelFactory.MainWindow = this;

        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = (DataContext as MainViewModel).Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel();
        }
    }
}
