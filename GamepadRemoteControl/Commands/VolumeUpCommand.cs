﻿using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteController;
using System;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "volumeup")]
    public sealed class VolumeUpCommand : ButtonCommand
    {
        private Action _action;
        [XmlIgnore]
        public override Action Action
        {
            get
            {
                if (_action == null)
                {
                    _action = new Action(()=>
                    {
                        InteropHelper.PushKeyboard(ConsoleKey.VolumeUp);
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
                return "Volume Up";
            }
        }
    }
}
