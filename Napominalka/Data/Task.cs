using System;
using System.Collections.ObjectModel;

namespace MaximStartsev.SmallUtilities.Napominalka.Data
{
    public sealed class Task
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public Int64 RemindEvery { get; set; }//todo возможно не поддерживается EnittyFramewrokом
        public string Creator { get; set; }
        public string Executor { get; set; }
        public TaskStatus Status { get; set; }
        public ObservableCollection<Comment> Comments { get; set; } = new ObservableCollection<Comment>();
    }
}