using System.Diagnostics;
using System.Windows;

namespace PowerSideDock.WPF.Views.MainWindow {
    public partial class MainWindow : Window {
        public MainWindow() {
            InitializeComponent();
        }

        private void Root_Activated(object sender, System.EventArgs e) {
            this.Activate();
        }
    }
}
