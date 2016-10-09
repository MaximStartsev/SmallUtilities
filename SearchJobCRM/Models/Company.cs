using System.Collections.ObjectModel;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Models
{
    class Company
    {
        public Company()
        {
            Vacancies = new ObservableCollection<Vacancy>();
        }

        public int Id { get; set; }

        /// <summary>
        /// Название компании
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Контактное лицо
        /// </summary>
        public string ContactPerson { get; set; }
        /// <summary>
        /// Телефон
        /// </summary>
        public string Phone { get; set; }
        /// <summary>
        /// Емейл
        /// </summary>
        public string Email { get; set; }
        /// <summary>
        /// Скайп
        /// </summary>
        public string Skype { get; set; }
        /// <summary>
        /// Текущая стадия диалога
        /// </summary>
        public string CurrentStage { get; set; }
        /// <summary>
        /// Комментарий
        /// </summary>
        public string Comment { get; set; }
        /// <summary>
        /// Список вакансий
        /// </summary>
        public virtual ObservableCollection<Vacancy> Vacancies { get; set; }
    }
}
