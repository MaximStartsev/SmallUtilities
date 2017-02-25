using MaximStartsev.GamepadRemoteControl.Meta;
using System;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "keyboardkey")]
    public class SendKeyboardKeyCommand : ButtonCommand
    {
        [CommandParameter]
        public string Key { get; set; }
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
                        SendKeys.SendWait(Key);
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
                return "Keyboard Key";
            }
        }
    }
}
