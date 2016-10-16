using MaximStartsev.SmallUtilities.FoodSelector.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MaximStartsev.SmallUtilities.FoodSelector.Utilities
{
    class DatabaseContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }
        public bool HasUnsavedChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
        }
    }
}
