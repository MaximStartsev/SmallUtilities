using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System.Data.Entity;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    class DatabaseContext:DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<DialogMessage> Messages { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder){  }
    }

}
