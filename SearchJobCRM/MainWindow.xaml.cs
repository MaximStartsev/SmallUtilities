using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            DataContext = new ViewModels.MainViewModel();
        }

        private void CompanyDataGrid_AddingNewItem(object sender, AddingNewItemEventArgs e)
        {
            (sender as DataGrid).CommitEdit();
        }
    }
}
