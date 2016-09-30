using System;
using System.Windows.Input;

namespace MaximStartsev.SmallUtilities.SearchJobCRM.Utilities
{
    class DelegateCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private Action<object> _callback;
        public DelegateCommand(Action<object> callback)
        {
            _callback = callback;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _callback(parameter);
        }
    }
}
