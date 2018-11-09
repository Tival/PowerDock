using Psd.Wpf.Views.Main;
using System.Windows;

namespace Psd.Wpf {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application {
        private void Application_Startup(object sender, StartupEventArgs e) {
            var mainWindow = new MainWindow();
            mainWindow.Show();
        }
    }
}
