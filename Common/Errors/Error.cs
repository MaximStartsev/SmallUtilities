using System;

namespace MaximStartsev.SmallUtilities.Common.Errors
{
    public class Error
    {
        public string Message { get; set; }
        public string StackTrace { get; set; }
        public Error(string message, string stacktrace)
        {
            Message = message;
            StackTrace = stacktrace;
        }
        public Error(Exception exception)
        {
            if (exception == null)
            {
                throw new ArgumentNullException("exception");
            }
            Message = exception.Message;
            StackTrace = exception.StackTrace;
        }
        private void LoadFromInnerException(Exception ex)
        {
            Message += ex.Message;
            StackTrace += ex.StackTrace;
            if (ex.InnerException != null)
            {
                Message += Environment.NewLine + "--------------" + Environment.NewLine;
                StackTrace += Environment.NewLine + "--------------" + Environment.NewLine;
                LoadFromInnerException(ex.InnerException);
            }
        }
        public override string ToString()
        {
            return Message;
        }
    }
}
