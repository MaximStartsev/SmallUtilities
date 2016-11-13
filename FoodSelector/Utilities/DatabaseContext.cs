using MaximStartsev.SmallUtilities.FoodSelector.Models;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Data.Entity;
using System.Linq;

namespace MaximStartsev.SmallUtilities.FoodSelector.Utilities
{
    class DatabaseContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }

        public ObservableCollection<Dish> DishesCollection { get; private set; }
        public ObservableCollection<Tag> TagsCollection { get; private set; }
        public ObservableCollection<Ingredient> IngredientsCollection { get; private set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) { }

        public void Load()
        {
            DishesCollection = new ObservableCollection<Dish>(Dishes.ToList());
            DishesCollection.CollectionChanged += DishesCollection_CollectionChanged;
            TagsCollection = new ObservableCollection<Tag>(Tags.ToList());
            TagsCollection.CollectionChanged += TagsCollection_CollectionChanged;
            IngredientsCollection = new ObservableCollection<Ingredient>(Ingredients.ToList());
            IngredientsCollection.CollectionChanged += IngredientsCollection_CollectionChanged;
        }

        private void IngredientsCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Ingredient item in e.NewItems)
                    {
                        Ingredients.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Ingredient item in e.OldItems)
                    {
                        Ingredients.Remove(item);
                    }
                    break;
            }
        }

        private void TagsCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Tag item in e.NewItems)
                    {
                        Tags.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Tag item in e.OldItems)
                    {
                        Tags.Remove(item);
                    }
                    break;
            }
        }

        private void DishesCollection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            switch (e.Action)
            {
                case NotifyCollectionChangedAction.Add:
                    foreach (Dish item in e.NewItems)
                    {
                        Dishes.Add(item);
                    }
                    break;
                case NotifyCollectionChangedAction.Remove:
                    foreach (Dish item in e.OldItems)
                    {
                        Dishes.Remove(item);
                    }
                    break;
            }
        }

        public bool HasUnsavedChanges()
        {
            return ChangeTracker.Entries().Any(e => e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted);
        }
    }
}
