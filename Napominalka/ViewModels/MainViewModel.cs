using MaximStartsev.SmallUtilities.Common.MVVM;
using MaximStartsev.SmallUtilities.Napominalka.Data;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace MaximStartsev.SmallUtilities.Napominalka.ViewModels
{
    internal sealed class MainViewModel:INotifyPropertyChanged
    {
        public ObservableCollection<Task> Tasks { get; private set; }
        public string CurrentUser { get; set; }
        public Task NewTask { get; set; } = new Task();
        public ICommand CreateTaskCommand { get; private set; } 
        private readonly DatabaseContext DatabaseContext;

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            CreateTaskCommand = new DelegateCommand(o => CreateTask());
            try
            {
                DatabaseContext = new DatabaseContext();
                Tasks = new ObservableCollection<Task>(DatabaseContext.Tasks.ToList());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        private void CreateTask()
        {
            try
            {
                ValidateTask(NewTask);
                if (String.IsNullOrEmpty(CurrentUser))
                {
                    throw new Exception("Не задано имя пользователя");
                }
                NewTask.Added = DateTime.Now;
                NewTask.Creator = CurrentUser;
                NewTask.Executor = CurrentUser;
                NewTask.Status = TaskStatus.Opened;
                Tasks.Add(NewTask);
                DatabaseContext.Tasks.Add(NewTask);
                NewTask = new Task();
                NotifyPropertyChanged(nameof(NewTask));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            Debug.WriteLine("create task");
        }
        private static void ValidateTask(Task task)
        {
            if (String.IsNullOrEmpty(task.Title))
            {
                throw new Exception("Заголовок не должен быть пуст");
            }
        }
        private void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
