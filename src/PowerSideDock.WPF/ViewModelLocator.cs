using GalaSoft.MvvmLight.Ioc;
using PowerSideDock.Database;
using PowerSideDock.WPF.EventEmiters;
using PowerSideDock.WPF.Views.MainWindow;

namespace PowerSideDock.WPF {
    public class ViewModelLocator {
        public ViewModelLocator() {
            SimpleIoc.Default.Register<MainWindowViewModel>();
            SimpleIoc.Default.Register<MouseTracker>();
            SimpleIoc.Default.Register<DbContext>();
            SimpleIoc.Default.Register<NoteRepository>();
        }

        public MainWindowViewModel MainWindowViewModel {
            get {
                return SimpleIoc.Default.GetInstance<MainWindowViewModel>();
            }
        }
    }
}
