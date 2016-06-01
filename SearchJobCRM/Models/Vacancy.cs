using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Models
{
    class Vacancy: ActiveRecordBase
    {
        [FieldAttribute]
        public int Id { get; private set; }
        /// <summary>
        /// Название вакансии
        /// </summary>
        [FieldAttribute]
        public string Title { get; set; }
        /// <summary>
        /// Компания
        /// </summary>
        [FieldAttribute]
        public Company Company { get; set; }
        /// <summary>
        /// Предлагаемая зарплата
        /// </summary>
        [FieldAttribute]
        public int? Salary { get; set; }
        /// <summary>
        /// Описание
        /// </summary>
        [FieldAttribute]
        public string Description { get; set; }
        /// <summary>
        /// Обязанности
        /// </summary>
        [FieldAttribute]
        public string Responsibilities { get; set; }
        /// <summary>
        /// Требуемые навыки
        /// </summary>
        [FieldAttribute]
        public string RequiredSkills { get; set; }
        /// <summary>
        /// Условия
        /// </summary>
        [FieldAttribute]
        public string Conditions { get; set; }
    }
}
