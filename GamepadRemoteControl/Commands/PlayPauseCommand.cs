using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteController;
using System;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "playpause")]
    public sealed class PlayPauseCommand : ButtonCommand
    {
        private Action _action;
        [XmlIgnore]
        public override Action Action
        {
            get
            {
                if (_action == null)
                {
                    _action = new Action(() =>
                    {
                        InteropHelper.PushKeyboard(ConsoleKey.MediaPlay);
                    });
                }
                return _action;
            }
        }
        [XmlIgnore]
        public override string Title
        {
            get
            {
                return "Play-Pause";
            }
        }
    }
}
