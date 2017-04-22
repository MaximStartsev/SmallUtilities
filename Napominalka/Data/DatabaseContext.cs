using System.Data.Entity;
using System.Linq;

namespace MaximStartsev.SmallUtilities.Napominalka.Data
{
    [DbConfigurationType(typeof(SQLiteConfiguration))]
    internal sealed class DatabaseContext : DbContext
    {
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Task> Tasks { get; set; }
        public DatabaseContext()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DatabaseContext>());
        }
        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
        public bool HasUnsavedChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
        }
    }
}
