using MaximStartsev.SmallUtilities.SearchJobCRM.Models;
using System;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    class RegisteredErrorArgs : EventArgs
    {
        public Error Error { get; private set; }
        public RegisteredErrorArgs(Error error)
        {
            Error = error;
        }
    }
    /// <summary>
    /// Класс для регистрации ошибок
    /// </summary>
    static class ErrorRegistrator
    {
        public static event EventHandler<RegisteredErrorArgs> RegisteredError;

        public static void Show(this Exception ex)
        {
            RegisteredError?.Invoke(null, new RegisteredErrorArgs(new Error(ex)));
        }
    }
}
