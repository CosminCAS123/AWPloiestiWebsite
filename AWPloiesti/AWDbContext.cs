using Microsoft.EntityFrameworkCore;
using AWPloiesti.Models;


namespace AWPloiesti
{
    public class AWDbContext : DbContext
    {
        public DbSet<Participant> Participants { get; set; }
        public DbSet<Tournament> Tournaments { get; set; }

        public string DbPath { get; }
        public AWDbContext()
        {
            var folder = Environment.SpecialFolder.LocalApplicationData;
            var path = Environment.GetFolderPath(folder);
            DbPath = Path.Join(path, "awploiesti.db");
        }

        // The following configures EF to create a Sqlite database file in the
        // special "local" folder for your platform.
        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseSqlite($"Data Source={DbPath}");
    }
}
