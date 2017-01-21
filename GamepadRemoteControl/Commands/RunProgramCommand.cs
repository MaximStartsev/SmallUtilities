using MaximStartsev.GamepadRemoteControl.Meta;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "runprogram")]
    internal sealed class RunProgramCommand : ICommand
    {
        [CommandParameter]
        private string ExeFile { get; set; }
        [XmlIgnore]
        private Action _action;
        public Action Action
        {
            get
            {
                if (_action != null)
                {
                    _action = new Action(() =>
                    {
                        Process.Start(ExeFile);
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
                return "Run program";
            }
        }
    }
}