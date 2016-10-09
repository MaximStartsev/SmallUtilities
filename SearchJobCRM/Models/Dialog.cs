using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Models
{
    class DialogMessage
    {
        public DialogMessage()
        {
            Date = DateTime.Now;
        }
        public int Id { get; set; }
        public Vacancy Vacancy{get;set;}
        public string Message { get; set; }
        public DateTime Date { get; set; }
    }
}
