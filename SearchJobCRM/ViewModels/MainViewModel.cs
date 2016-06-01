using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels
{
    class MainViewModel:INotifyPropertyChanged
    {
        public String StatusBar { get; private set; }
        public List<Vacancy> Vacancies;
        public MainViewModel()
        {
            StatusBar = "Готово";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
