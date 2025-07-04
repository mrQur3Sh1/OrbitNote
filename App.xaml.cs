using System.Text;
using System.Windows;
using NotebookMVVM.Ui.Desktop;

namespace NotebookMVVM
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            base.OnStartup(e);

            var welcome = new WelcomeWindow();
            welcome.Show();
        }
    }




}
