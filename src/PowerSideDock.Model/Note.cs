namespace PowerSideDock.Model {
    public class Note : Dbo {
        private string title;
        public string Title {
            get {
                return title;
            }
            set {
                title = value;
                wasChanged = true;
            }
        }

        private string content;
        public string Content {
            get {
                return content;
            }
            set {
                content = value;
                wasChanged = true;
            }
        }

        private bool wasChanged { get; set; }
        public void ResetChangeTracker() {
            wasChanged = false;
        }

        public bool WasChanged() {
            return wasChanged;
        }
    }
}
