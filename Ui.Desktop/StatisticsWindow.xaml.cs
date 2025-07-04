using System.Windows;
using NotebookMVVM.Logic.Ui;

namespace NotebookMVVM.Ui.Desktop
{
    public partial class StatisticsWindow : Window
    {
        public StatisticsWindow(StatisticsViewModel viewModel)
        {
            InitializeComponent();
            DataContext = viewModel; 
        }
    }
}
