using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [XmlInclude(typeof(MoveToPrevMonitorCommand))]
    [XmlInclude(typeof(MoveToNextMonitorCommand))]
    [XmlInclude(typeof(SendKeyboardKeyCommand))]
    [XmlInclude(typeof(VolumeUpCommand))]
    [XmlInclude(typeof(VolumeDownCommand))]
    [XmlInclude(typeof(PlayPauseCommand))]
    [XmlInclude(typeof(RunProgramCommand))]
    [XmlInclude(typeof(MouseStickCommand))]
    [XmlInclude(typeof(MouseClickCommand))]
    [XmlInclude(typeof(ScrollStickCommand))]
    public abstract class Command
    {
        public abstract string Title { get; }
    }
}
