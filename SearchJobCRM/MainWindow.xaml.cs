using MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels;
using System.Windows;
using System.Windows.Controls;

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
            DataContext = new MainViewModel();
        }

        private void CompanyDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            (sender as DataGrid).CommitEdit();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
