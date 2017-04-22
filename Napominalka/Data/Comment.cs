using System;

namespace MaximStartsev.SmallUtilities.Napominalka.Data
{
    public sealed class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public DateTime Added { get; set; } = DateTime.Now;
        public string Creator { get; set; }
    }
}
