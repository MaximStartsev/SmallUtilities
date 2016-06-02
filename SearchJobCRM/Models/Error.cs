using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Models
{
    class Error
    {
        public string Title { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Error(string title, string message, string stacktrace)
        {
            Title = title;
            Message = message;
            StackTrace = stacktrace;
        }
        public Error(string title, Exception exception)
        {
            Title = title;
            if (exception != null)
            {
                Message = exception.Message;
                StackTrace = exception.StackTrace;
            }
            else
            {
                Message = String.Empty;
                StackTrace = String.Empty;
            }
        }
    }
}
