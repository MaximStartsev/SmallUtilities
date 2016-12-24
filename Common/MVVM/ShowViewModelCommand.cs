using System;
using System.Collections;
using System.Windows.Input;
using System.Linq;

namespace MaximStartsev.SmallUtilities.Common.MVVM
{
    class ShowViewModelCommand : ICommand
    {
        public event EventHandler CanExecuteChanged;
        private readonly Type _type;

        public ShowViewModelCommand(Type type)
        {
            _type = type;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            object[] arg = null;
            if(parameter != null)
            {
                if(parameter as object[] != null)
                {
                    arg = parameter as object[];
                }
                else
                {
                    arg = new object[] { parameter };
                }
            }
          //  Activator.CreateInstance(_type, parameter);
            var viewModel = CreateInstance(_type, parameter);
            //var viewModel = arg == null ? Activator.CreateInstance(_type) as ViewModel : Activator.CreateInstance(_type, arg) as ViewModel;
            if (viewModel == null)
            {
                throw new Exception(String.Format("Тип {0} не является вьюмоделью", _type.FullName));
            }
            //var viewModelInstance = Parameters == null ? Activator.CreateInstance(ViewModel) as ViewModel : Activator.CreateInstance(ViewModel, Parameters) as ViewModel;
            //if (viewModelInstance == null)
            //{
            //    throw new Exception(String.Format("Тип {0} не является вьюмоделью", ViewModel.FullName));
            //}
            //viewModelInstance.Show();

            viewModel.Show();
        }
        private ViewModel CreateInstance(Type t, params object[] paramArray)
        {
            return Activator.CreateInstance(t, args: paramArray) as ViewModel;
        }
    }
}
