using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteController;
using System;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "volumedown")]
    internal sealed class VolumeDownCommand : ICommand
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
                        InteropHelper.PushKeyboard(ConsoleKey.VolumeDown);
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
                return "Volume Down";
            }
        }
    }
}
