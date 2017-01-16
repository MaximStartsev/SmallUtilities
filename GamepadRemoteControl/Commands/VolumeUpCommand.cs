﻿using MaximStartsev.GamePadRemoteController;
using System;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    class VolumeUpCommand : ICommand
    {
        private Action _action;
        [XmlIgnore]
        public Action Action
        {
            get
            {
                if (_action != null)
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
        public string Title
        {
            get
            {
                return "Volume Up";
            }
        }
    }
}
