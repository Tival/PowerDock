using System;
using System.Collections.Specialized;
using System.IO;
using System.Windows;
using Psd.Wpf.EventEmiters;
namespace Psd.Wpf.Views.Main {
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window {
        public MouseTracker mousePosition;
        public MainWindowState state;
        public MainWindow() {
            InitializeComponent();
            state = new MainWindowState();
            state.Shortcuts.CollectionChanged += OnUpdateShortcuts;
            DataContext = state;

            Width = 300;
            Height = SystemParameters.VirtualScreenHeight;

            Top = 0;
            Left = SystemParameters.VirtualScreenWidth - this.Width;

            var xMin = SystemParameters.VirtualScreenWidth - this.Width;
            var xMax = SystemParameters.VirtualScreenWidth;
            var yMin = 0;
            var yMax = SystemParameters.VirtualScreenHeight;

            mousePosition = new MouseTracker();
            mousePosition.OnCursorInside += MouseInPosition;
            mousePosition.OnCursorOutside += MouseOutPosition;
            mousePosition.RegisterPositionRectangle(xMin, xMax, yMin, yMax);
            mousePosition.StartTracking();
        }

        private void OnUpdateShortcuts(object sender, NotifyCollectionChangedEventArgs e) {
            ShortcutsListBox.ItemsSource = null;
            ShortcutsListBox.ItemsSource = state.Shortcuts;
        }

        private void MouseOutPosition(double x, double y, PositionRectangle position) {
            if (this.Visibility != Visibility.Collapsed)
                this.Visibility = Visibility.Collapsed;
        }

        private void MouseInPosition(double x, double y, PositionRectangle position) {
            if (x > (position.XMax - 5)) {
                if (this.Visibility != Visibility.Visible)
                    this.Visibility = Visibility.Visible;
            }
        }

        private void Grid_Drop(object sender, DragEventArgs e) {
            var paths = (string[])e.Data.GetData(DataFormats.FileDrop);

            foreach(var path in paths) {
                var fileName = Path.GetFileName(path);

                state.Shortcuts.Add(new Shortcut() {
                    Icon = null,
                    Name = fileName,
                    Path = path
                });
            }
        }
    }
}
