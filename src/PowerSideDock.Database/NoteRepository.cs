using PowerSideDock.Model;

namespace PowerSideDock.Database {
    public class NoteRepository : GenericRepository<Note> {

        public NoteRepository(DbContext context) : base(context) {
        }
    }
}
