using System.Windows;
using System.Windows.Input;
using NotebookMVVM.Ui.Desktop;
using CommunityToolkit.Mvvm.Input;


namespace NotebookMVVM.Logic.Ui
{
    public class WelcomeViewModel : ViewModelBase
    {
        public ICommand StartCommand { get; }

        public WelcomeViewModel()
        {
            StartCommand = new RelayCommand(OpenMainWindow);
        }

        private void OpenMainWindow()
        {
            var mainWindow = new MainWindow();
            mainWindow.Show();

            foreach (Window window in Application.Current.Windows)
            {
                if (window is WelcomeWindow)
                {
                    window.Close();
                    break;
                }
            }
        }
    }
}
