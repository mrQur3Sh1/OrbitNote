using System.Windows;
using NotebookMVVM.Logic.Ui;

namespace NotebookMVVM.Ui.Desktop 
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent(); 
            DataContext = new MainViewModel();
        }
    }
}
