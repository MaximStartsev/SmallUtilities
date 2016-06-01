using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Models
{
    class Vacancy: ActiveRecordBase
    {
        public int Id { get; private set; }
        /// <summary>
        /// Название вакансии
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Компания
        /// </summary>
        public Company Company { get; set; }
        /// <summary>
        /// Предлагаемая зарплата
        /// </summary>
        public int? Salary { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }
        /// <summary>
        /// Обязанности
        /// </summary>
        public string Responsibilities { get; set; }
        /// <summary>
        /// Требуемые навыки
        /// </summary>
        public string RequiredSkills { get; set; }
        /// <summary>
        /// Условия
        /// </summary>
        public string Conditions { get; set; }
    }
}
