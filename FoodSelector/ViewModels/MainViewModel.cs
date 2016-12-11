using MaximStartsev.SmallUtilities.Common.Errors;
using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.FoodSelector.Models;
using MaximStartsev.SmallUtilities.FoodSelector.Utilities;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows.Input;

namespace MaximStartsev.SmallUtilities.FoodSelector.ViewModels
{
    class MainViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Dish> Dishes { get; private set; }
        public ObservableCollection<Tag> Tags { get; private set; }
        public ObservableCollection<Ingredient> Ingredients { get; private set; }
        private DatabaseContext _dbContext;
        private Dish _selectedDish;
        public Dish SelectedDish
        {
            get { return _selectedDish; }
            set
            {
                if(_selectedDish != value)
                {
                    _selectedDish = value;
                    InvokePropertyChanged(nameof(SelectedDish));
                }
            }
        }
        public string TagName { get; set; }
        public DishCreatorViewModel DishCreator { get; private set; }
        public ICommand RandomDishCommand { get; private set; }
        public ICommand RemoveDishCommand { get; private set; }
        static string GetIntBinaryString(int n)
        {
            char[] b = new char[32];
            int pos = 31;
            int i = 0;

            while (i < 32)
            {
                if ((n & (1 << i)) != 0)
                {
                    b[pos] = '1';
                }
                else
                {
                    b[pos] = '0';
                }
                pos--;
                i++;
            }
            return new string(b);
        }

        public MainViewModel()
        {
            try
            {
                var error = new ErrorViewModel();
                _dbContext = new DatabaseContext();
                _dbContext.Load();
                Dishes = _dbContext.DishesCollection;
                Tags = _dbContext.TagsCollection;
                Ingredients = _dbContext.IngredientsCollection;
                DishCreator = new DishCreatorViewModel(Dishes, Tags, Ingredients);
                RandomDishCommand = new DelegateCommand(o => RandomDish());
                RemoveDishCommand = new DelegateCommand(o => RemoveDish(o as Dish));
            }
            catch (Exception ex)
            {
                ex.Show();
            }
        }

        private void RemoveDish(Dish dish)
        {
            if (dish != null)
            {
                Dishes.Remove(dish);
            }
        }
        public void Close()
        {
            _dbContext.SaveChanges();
        }

        public void RandomDish()
        {
            int value = 0;
            var maxCount = Dishes.Max(d => d.Count) + 1;
            var rand = new Random();
            foreach (var dish in Dishes.Where(d=>String.IsNullOrEmpty(TagName) || d.Tags.Any(t=>t.Name == TagName)))
            {
                var newValue = rand.Next(1, maxCount - dish.Count);
                if(newValue > value)
                {
                    value = newValue;
                    SelectedDish = dish;
                }
            }
            SelectedDish.Count++;
            InvokePropertyChanged(nameof(SelectedDish));
            _dbContext.SaveChangesAsync();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
