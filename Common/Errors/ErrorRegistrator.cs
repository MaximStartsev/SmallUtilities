using System;

namespace MaximStartsev.SmallUtilities.Common.Errors
{
    public class RegisteredErrorArgs : EventArgs
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
    public static class ErrorRegistrator
    {
        public static event EventHandler<RegisteredErrorArgs> RegisteredError;

        public static void Show(this Exception ex)
        {
            RegisteredError?.Invoke(null, new RegisteredErrorArgs(new Error(ex)));
        }
    }
}
