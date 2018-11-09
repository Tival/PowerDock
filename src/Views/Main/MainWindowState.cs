using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Windows.Media.Imaging;

namespace Psd.Wpf.Views.Main {
    public class Shortcut {
        public string Name { get; set; }
        public string Path { get; set; }
        public BitmapImage Icon { get; set; }
    }

    public class MainWindowState {
        private Shortcut _selectedShortcut;

        public ObservableCollection<Shortcut> Shortcuts { get; set; }
        public Shortcut SelectedShortcut {
            get {
                return _selectedShortcut;
            }
            set {
                if (value != null) {
                    if (File.Exists(value.Path))
                        Process.Start(value.Path);

                    _selectedShortcut = value;
                }
            }
        }

        public MainWindowState() {
            Shortcuts = new ObservableCollection<Shortcut>();
        }
    }
}
