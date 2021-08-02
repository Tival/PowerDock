using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using PowerSideDock.Database;
using PowerSideDock.Model;
using PowerSideDock.WPF.EventEmiters;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PowerSideDock.WPF.Views.MainWindow {
    public class MainWindowViewModel : ViewModelBase {
        public ObservableCollection<Note> NoteList { get; set; }

        private readonly MouseTracker mouseTracker;
        private readonly NoteRepository noteRepository;

        public MainWindowViewModel(MouseTracker mouseTracker, NoteRepository noteRepository) {
            this.mouseTracker = mouseTracker;
            this.noteRepository = noteRepository;

            NoteList = new ObservableCollection<Note>();
            InitializeWindowPlacement();
            InitializeMouseTracker();
            RepositoryLoadNotes();
        }

        private double windowWidth;
        public double WindowWidth {
            get {
                return windowWidth;
            }
            set {
                windowWidth = value;
                this.RaisePropertyChanged("WindowWidth");
            }
        }

        private double windowHeight;
        public double WindowHeight {
            get {
                return windowHeight;
            }
            set {
                windowHeight = value;
                this.RaisePropertyChanged("WindowHeight");
            }
        }

        private double windowTop;
        public double WindowTop {
            get {
                return windowTop;
            }
            set {
                windowTop = value;
                this.RaisePropertyChanged("WindowTop");
            }
        }

        private double windowLeft;
        public double WindowLeft {
            get {
                return windowLeft;
            }
            set {
                windowLeft = value;
                this.RaisePropertyChanged("WindowLeft");
            }
        }

        private bool windowVisible;
        public bool WindowVisible {
            get {
                return windowVisible;
            }
            set {
                windowVisible = value;
                this.RaisePropertyChanged("WindowVisible");
            }
        }

        private ICommand createNoteCommand;
        public ICommand CreateNoteCommand {
            get {
                if (createNoteCommand == null) {
                    createNoteCommand = new RelayCommand(CreateNote);
                }
                return createNoteCommand;
            }
        }

        private ICommand deleteNoteCommand;
        public ICommand DeleteNoteCommand {
            get {
                if (deleteNoteCommand == null) {
                    deleteNoteCommand = new RelayCommand<Note>(DeleteNote);
                }
                return deleteNoteCommand;
            }
        }

        private void DeleteNote(Note note) {
            NoteList.Remove(note);
            noteRepository.Remove(note);
        }

        private void CreateNote() {
            var note = new Note() {
                Title = "Hello world",
                Content = "Hello world of content!"
            };

            noteRepository.Create(note);
            NoteList.Add(note);
        }

        private void InitializeWindowPlacement() {
            WindowWidth = 300;
            WindowHeight = SystemParameters.VirtualScreenHeight;
            WindowTop = 0;
            WindowLeft = SystemParameters.VirtualScreenWidth - WindowWidth;
            WindowVisible = false;
        }

        private void InitializeMouseTracker() {
            var xMin = SystemParameters.VirtualScreenWidth - this.WindowWidth;
            var xMax = SystemParameters.VirtualScreenWidth;
            var yMin = 0;
            var yMax = SystemParameters.VirtualScreenHeight;

            mouseTracker.OnCursorInside += MouseInPosition;
            mouseTracker.OnCursorOutside += MouseOutPosition;
            mouseTracker.RegisterPositionRectangle(xMin, xMax, yMin, yMax);
            mouseTracker.StartTracking();
        }

        private void RepositoryLoadNotes() {
            NoteList = new ObservableCollection<Note>(noteRepository.Find(x => true));
            RaisePropertyChanged("NoteList");
        }

        private void MouseOutPosition(double x, double y, PositionRectangle position) {
            if (this.WindowVisible != false) {
                this.WindowVisible = false;

                var updatedNotes = NoteList.Where(x => x.WasChanged()).ToList();
                noteRepository.Update(updatedNotes);
                updatedNotes.ForEach(note => note.ResetChangeTracker());
            }
        }

        private void MouseInPosition(double x, double y, PositionRectangle position) {
            if (x > (position.XMax - 5)) {
                if (this.WindowVisible != true)
                    this.WindowVisible = true;
            }
        }
    }
}
