﻿using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.FoodSelector.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaximStartsev.SmallUtilities.FoodSelector.ViewModels
{
    internal sealed class DishCreatorViewModel:INotifyPropertyChanged
    {
        private ObservableCollection<Dish> _dishesCollection;
        public ObservableCollection<Tag> AllTags { get; private set; }
        public ObservableCollection<Ingredient> AllIngredients { get; private set; }
        private Dish _currentDish;
        public bool IsCanSave { get { return !String.IsNullOrEmpty(Title); } }
        public string Title
        {
            get { return _currentDish.Title; }
            set
            {
                if(_currentDish.Title != value)
                {
                    _currentDish.Title = value;
                    InvokePropertyChanged(nameof(IsCanSave));
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

        public ObservableCollection<Ingredient> Ingredients { get; private set; }

        public ObservableCollection<Tag> Tags { get; private set; }
        private string _newIngredient;
        public string NewIngredient
        {
            get { return _newIngredient; }
            set
            {
                if(_newIngredient != value)
                {
                    _newIngredient = value;
                    InvokePropertyChanged(nameof(NewIngredient));
                }
            }
        }
        private string _newTag;
        public string NewTag
        {
            get { return _newTag; }
            set
            {
                if (_newTag != value)
                {
                    _newTag = value;
                    InvokePropertyChanged(nameof(NewTag));
                }
            }
        }

        public ICommand AddIngredientCommand { get; private set; }
        public ICommand AddTagCommand { get; private set; }
        public ICommand SaveCommand { get; private set; }


        public DishCreatorViewModel(ObservableCollection<Dish> dishesCollection, ObservableCollection<Tag> tags, ObservableCollection<Ingredient> ingredients)
        {
            _dishesCollection = dishesCollection;
            AllTags = tags;
            AllIngredients = ingredients;
            _currentDish = new Dish();
            AddIngredientCommand = new DelegateCommand(o => AddIngredient());
            AddTagCommand = new DelegateCommand(o => AddTag());
            SaveCommand = new DelegateCommand(o=>Save());
            Tags = new ObservableCollection<Tag>(_currentDish.Tags);
            Ingredients = new ObservableCollection<Ingredient>(_currentDish.Ingredient);
        }
        private void AddIngredient()
        {
            if (!AllIngredients.Any(i => i.Name == NewIngredient))
            {
                AllIngredients.Add(new Ingredient { Name = NewIngredient });
            }
            Ingredients.Add(AllIngredients.First(i => i.Name == NewIngredient));
            NewIngredient = String.Empty;
        }
        private void AddTag()
        {
            if(!AllTags.Any(t=>t.Name == NewTag))
            {
                AllTags.Add(new Tag() { Name = NewTag });
            }
            Tags.Add(AllTags.First(t => t.Name == NewTag));
            NewTag = String.Empty;
        }
        private void Save()
        {
            if(_dishesCollection.Any(d=>d.Title.ToLower() == Title.ToLower()))
            {
                MessageBox.Show("Блюдо с таким именем уже существует");
            }
            else
            {
                _dishesCollection.Add(_currentDish);
                _currentDish = new Dish();
                Tags = new ObservableCollection<Tag>(_currentDish.Tags);
                Ingredients = new ObservableCollection<Ingredient>(_currentDish.Ingredient);
                InvokePropertyChanged(nameof(Title));
                InvokePropertyChanged(nameof(Type));
                NewIngredient = String.Empty;
                InvokePropertyChanged(nameof(Ingredients));
                NewTag = String.Empty;
                InvokePropertyChanged(nameof(Tags));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
