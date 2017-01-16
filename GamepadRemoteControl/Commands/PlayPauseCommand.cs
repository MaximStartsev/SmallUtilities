using MaximStartsev.GamePadRemoteController;
using System;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    internal sealed class PlayPauseCommand : ICommand
    {
        private Action _action;
        [XmlIgnore]
        public Action Action
        {
            get
            {
                if (_action != null)
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
        public string Title
        {
            get
            {
                return "Play-Pause";
            }
        }
    }
}
