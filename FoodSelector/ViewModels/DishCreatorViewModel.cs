using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.FoodSelector.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace MaximStartsev.SmallUtilities.FoodSelector.ViewModels
{
    internal sealed class DishCreatorViewModel
    {
        private ObservableCollection<Dish> _dishesCollection;
        private Dish _currentDish;

        public string Title
        {
            get { return _currentDish.Title; }
            set
            {
                if(_currentDish.Title != value)
                {
                    _currentDish.Title = value;
                }
            }
        }

        public DishType Type
        {
            get { return _currentDish.Type; }
            set
            {
                if(_currentDish.Type != value)
                {
                    _currentDish.Type = value;
                }
            }
        }

        public IEnumerable<Ingredient> Ingredients { get { return _currentDish.Ingredient; } }

        public IEnumerable<Tag> Tags { get { return _currentDish.Tags; } }
        public string NewIngredient { get; set; }
        public string NewTag { get; set; }

        public ICommand AddIngredientCommand { get; private set; }
        public ICommand AddTagCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }

        public DishCreatorViewModel(ObservableCollection<Dish> dishesCollection)
        {
            _dishesCollection = dishesCollection;
            _currentDish = new Dish();
            AddIngredientCommand = new DelegateCommand(o => AddIngredient());
            AddTagCommand = new DelegateCommand(o => AddTag());
            SaveCommand = new DelegateCommand(o=>Save());
        }
        private void AddIngredient()
        {

        }
        private void AddTag()
        {

        }
        private void Save()
        {

        }
    }
}
