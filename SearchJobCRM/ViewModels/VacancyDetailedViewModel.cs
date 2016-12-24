using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;
using System.ComponentModel;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels
{
    class VacancyDetailedViewModel : ViewModel, INotifyPropertyChanged
    {
        public override string Title { get; set; }

        public Vacancy Vacancy { get; set; }

        public override double DefaultWidth { get { return 800; } }

        public override double DefaultHeight { get { return 380; } }
        public VacancyDetailedViewModel()
        {

        }

        public VacancyDetailedViewModel(Vacancy vacancy)
        {
            Vacancy = vacancy;
            Title = String.Format("Вакансия {0}", Vacancy.Title);
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
