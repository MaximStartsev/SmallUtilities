using MaximStartsev.SmallUtilities.Napominalka.ViewModels;
using System.Windows;

//Мне лень придумывать клёвое название, поэтому назвал так
namespace MaximStartsev.SmallUtilities.Napominalka
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}
