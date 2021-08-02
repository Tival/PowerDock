using LiteDB;
using System.IO;

namespace PowerSideDock.Database {
    public class DbContext {
        public readonly LiteDatabase database;

        public DbContext() {
            var workingDir = Directory.GetCurrentDirectory();
            var dbPath = Path.Combine(workingDir, "data.db");

            database = new LiteDatabase(dbPath);
        }
    }
}
