using System.Collections.Generic;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Models
{
    class Vacancy
    {
        public int Id { get; set; }
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
        public int Salary { get; set; }
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
        public virtual IEnumerable<DialogMessage> Dialog { get; set; }
    }
    public class Student
    {
        public Student() { }

        public int StudentId { get; set; }
        public string StudentName { get; set; }

        public virtual Standard Standard { get; set; }
    }

    public class Standard
    {
        public Standard()
        {
            Students = new List<Student>();
        }
        public int StandardId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Student> Students { get; set; }
    }
}
