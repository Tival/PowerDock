using System.Collections.Specialized;
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
            state.Notes.CollectionChanged += OnUpdateNotes;
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

        private void OnUpdateNotes(object sender, NotifyCollectionChangedEventArgs e) {
            NotesListBox.ItemsSource = null;
            NotesListBox.ItemsSource = state.Notes;
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

        private void CreateNoteButton_Click(object sender, RoutedEventArgs e) {
            state.Notes.Add(new Note() {
                Title = "Hello world",
                Content = "Hello world of content"
            });
        }
    }
}
