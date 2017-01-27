using MaximStartsev.GamepadRemoteControl.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.MVC.Main
{
    public class MainModel
    {
        public Command ButtonA { get; set; }
        public Command ButtonB { get; set; }
        public Command ButtonX { get; set; }
        public Command ButtonY { get; set; }
        public Command ButtonLeftBumper { get; set; }
        public Command ButtonRightBumper { get; set; }
        public Command ButtonLeftTrigger { get; set; }
        public Command ButtonRightTrigger { get; set; }
        public Command ButtonSelect { get; set; }
        public Command ButtonStart { get; set; }
        public Command ButtonDPadUp { get; set; }
        public Command ButtonDPadLeft { get; set; }
        public Command ButtonDPadRight { get; set; }
        public Command ButtonDPadBottom { get; set; }
        public Command RightButtonStick { get; set; }
        public Command LeftButtonStick { get; set; }
        public StickCommand LeftStick { get; set; }
        public StickCommand RightStick { get; set; }
        [XmlIgnore]
        public List<Type> Commands { get; set; }

        public MainModel()
        {
            Commands = Assembly.GetEntryAssembly().GetTypes().Where(t => typeof(Command).IsAssignableFrom(t) && t.Name != nameof(Command) && t.Name != nameof(StickCommand)).ToList();
            Load();
        }
        private void Load()
        {
            //todo init
        }
    }
}
