using MaximStartsev.SmallUtilities.SearchJobCRM.Core;
using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.ViewModels
{
    class VacancyDetailedViewModel : ViewModel
    {
        public override string Title
        {
            get
            {
                return String.Format("Вакансия {0}",Vacancy.Title);
            }
            set
            {

            }
        }

        public Vacancy Vacancy { get; set; }

        public VacancyDetailedViewModel(Vacancy vacancy)
        {
            Vacancy = vacancy;
        }
    }
}
