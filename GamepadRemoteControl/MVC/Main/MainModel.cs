using MaximStartsev.GamepadRemoteControl.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.MVC.Main
{
    public sealed class MainModel
    {
        public ButtonCommand ButtonA { get; set; }
        public ButtonCommand ButtonB { get; set; }
        public ButtonCommand ButtonX { get; set; }
        public ButtonCommand ButtonY { get; set; }
        public ButtonCommand ButtonLeftBumper { get; set; }
        public ButtonCommand ButtonRightBumper { get; set; }
        public ButtonCommand ButtonLeftTrigger { get; set; }
        public ButtonCommand ButtonRightTrigger { get; set; }
        public ButtonCommand ButtonSelect { get; set; }
        public ButtonCommand ButtonStart { get; set; }
        public ButtonCommand ButtonDPadUp { get; set; }
        public ButtonCommand ButtonDPadLeft { get; set; }
        public ButtonCommand ButtonDPadRight { get; set; }
        public ButtonCommand ButtonDPadBottom { get; set; }
        public ButtonCommand RightButtonStick { get; set; }
        public ButtonCommand LeftButtonStick { get; set; }
        public StickCommand LeftStick { get; set; }
        public StickCommand RightStick { get; set; }
        [XmlIgnore]
        public List<Type> Commands { get; set; }

        public MainModel()
        {
            Commands = Assembly.GetEntryAssembly().GetTypes().Where(t => typeof(Command).IsAssignableFrom(t) && !t.IsAbstract).ToList();
        }
    }
}
