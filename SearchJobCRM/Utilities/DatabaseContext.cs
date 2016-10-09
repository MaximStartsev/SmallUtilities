using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System.Data.Entity;
using System.Linq;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    class DatabaseContext:DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<DialogMessage> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){  }
        public bool HasUnsavedChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
        }
    }

}
