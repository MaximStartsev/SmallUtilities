using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.FoodSelector.ViewModels;
using System.Windows;

namespace MaximStartsev.SmallUtilities.FoodSelector
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

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            DataContext = new MainViewModel();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            (DataContext as MainViewModel).Close();
        }
    }
}
