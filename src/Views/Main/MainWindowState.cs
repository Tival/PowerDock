using System.Collections.ObjectModel;

namespace Psd.Wpf.Views.Main {
    public class Note {
        public string Title { get; set; }
        public string Content { get; set; }
    }

    public class MainWindowState {
        private Note _selectedNote;

        public ObservableCollection<Note> Notes { get; set; }
        public Note SelectedNote {
            get {
                return _selectedNote;
            }
            set {
                if (value != null) {
                    _selectedNote = value;
                }
            }
        }

        public MainWindowState() {
            Notes = new ObservableCollection<Note>();
        }
    }
}
