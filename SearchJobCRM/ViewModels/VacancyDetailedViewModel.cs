using MaximStartsev.SmallUtilities.SearchJobCRM.Core;
using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels
{
    class VacancyDetailedViewModel : ViewModel
    {
        public override string Title { get; set; }

        public Vacancy Vacancy { get; set; }

        public override double DefaultWidth { get { return 800; } }

        public override double DefaultHeight { get { return 380; } }

        public VacancyDetailedViewModel(Vacancy vacancy)
        {
            Vacancy = vacancy;
            Title = String.Format("Вакансия {0}", Vacancy.Title);
        }
    }
}
