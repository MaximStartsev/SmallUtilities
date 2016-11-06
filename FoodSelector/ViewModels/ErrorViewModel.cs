using MaximStartsev.SmallUtilities.Common.Errors;
using MaximStartsev.SmallUtilities.Common.MVVM;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace MaximStartsev.SmallUtilities.FoodSelector.ViewModels
{
    class ErrorViewModel : ViewModel, INotifyPropertyChanged
    {
        public ObservableCollection<Error> Errors { get; set; }
        public override string Title { get; set; }
        private Error _currentError;
        public Error CurrentError
        {
            get { return _currentError; }
            set
            {
                if (_currentError != value)
                {
                    _currentError = value;
                    InvokePropertyChanged(nameof(CurrentError));
                    InvokePropertyChanged(nameof(IsStackTraceExists));
                }
            }
        }
        public bool IsStackTraceExists
        {
            get
            {
                return CurrentError != null && !String.IsNullOrEmpty(CurrentError.StackTrace);
            }
        }

        public override double DefaultWidth { get { return 500; } }

        public override double DefaultHeight { get { return 300; } }

        public ErrorViewModel()
        {
            Errors = new ObservableCollection<Error>();
            Title = "Ошибка";
            ErrorRegistrator.RegisteredError += ErrorRegistrator_RegisteredError;
        }


        private void ErrorRegistrator_RegisteredError(object sender, RegisteredErrorArgs e)
        {
            try
            {
                Errors.Add(e.Error);
                CurrentError = e.Error;
                Show();
            }
            catch (Exception ex)
            {
                //Если не удалось отобразить ошибку нормально, выводим обычный MessageBox
                MessageBox.Show(ex.ToString(), "Ошибка");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        private void InvokePropertyChanged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
    }
}
