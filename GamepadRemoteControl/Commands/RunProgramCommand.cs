using MaximStartsev.GamepadRemoteControl.Meta;
using System;
using System.Diagnostics;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "runprogram")]
    public sealed class RunProgramCommand : Command
    {
        [CommandParameter]
        private string ExeFile { get; set; }
        [XmlIgnore]
        private Action _action;
        public override Action Action
        {
            get
            {
                if (_action == null)
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
        public override string Title
        {
            get
            {
                return "Run program";
            }
        }
    }
}