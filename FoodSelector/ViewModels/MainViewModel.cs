using MaximStartsev.SmallUtilities.Common.Errors;
using MaximStartsev.SmallUtilities.FoodSelector.Models;
using MaximStartsev.SmallUtilities.FoodSelector.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;

namespace MaximStartsev.SmallUtilities.FoodSelector.ViewModels
{
    class MainViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Dish> Dishes { get; private set; }
        private DatabaseContext _dbContext;

        public Dish SelectedDish { get; private set; }
        public DishCreatorViewModel DishCreator { get; private set; }
        public MainViewModel()
        {
            try
            {
                var error = new ErrorViewModel();
                _dbContext = new DatabaseContext();
                _dbContext.SaveChanges();
                Dishes = new ObservableCollection<Dish>(_dbContext.Dishes.ToList());
                Dishes.CollectionChanged += Dishes_CollectionChanged;
                DishCreator = new DishCreatorViewModel(Dishes);
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }

        private void Dishes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
        }

        public void RandomDish()
        {
            Dish selectedDish = null;
            int value = 0;
            var maxCount = Dishes.Max(d => d.Count) + 1;
            var rand = new Random();
            foreach (var dish in Dishes)
            {
                var newValue = rand.Next(1, maxCount - dish.Count);
                if(newValue > value)
                {
                    value = newValue;
                    selectedDish = dish;
                }
            }
            selectedDish.Count++;
            _dbContext.SaveChangesAsync();
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
