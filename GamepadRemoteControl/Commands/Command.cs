using System;
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
    [XmlInclude(typeof(StickCommand))]
    public abstract class Command
    {
        public abstract string Title { get; }
        public abstract Action Action { get; }
    }
}
