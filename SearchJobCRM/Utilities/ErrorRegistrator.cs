using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    class RegisteredErrorArgs : EventArgs
    {
        public object Sender { get; private set; }
        public Error Error { get; private set; }
        public RegisteredErrorArgs(object sender, Error error)
        {
            Sender = sender;
            Error = error;
        }
    }
    /// <summary>
    /// Класс для регистрации ошибок
    /// </summary>
    static class ErrorRegistrator
    {
        public static event EventHandler<RegisteredErrorArgs> RegisteredError;

        public static void RegisterError(this object sender, Error error)
        {
            RegisteredError?.Invoke(null, new RegisteredErrorArgs(sender, error));
        }
        public static void RegisterError(this object sender, string title, string message, string stacktrace)
        {
            RegisterError(sender, new Error(title, message, stacktrace));
        }
        
    }
}
