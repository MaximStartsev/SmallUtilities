using MaximStartsev.GamepadRemoteControl.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MaximStartsev.GamepadRemoteControl
{
    internal class MainModel
    {
        public ICommand ButtonA { get; set; }
        public ICommand ButtonB { get; set; }
        public ICommand ButtonC { get; set; }
        public ICommand ButtonD { get; set; }
        public ICommand ButtonLeftBumper { get; set; }
        public ICommand ButtonRightBumper { get; set; }
        public ICommand ButtonLeftTrigger { get; set; }
        public ICommand ButtonRightTrigger { get; set; }
        public ICommand ButtonSelect { get; set; }
        public ICommand ButtonStart { get; set; }
        public ICommand ButtonDPadUp { get; set; }
        public ICommand ButtonDPadLeft { get; set; }
        public ICommand ButtonDPadRight { get; set; }
        public ICommand ButtonDPadBottom { get; set; }
        public ICommand RightButtonStick { get; set; }
        public ICommand LeftButtonStick { get; set; }
        public IStickCommand LeftStick { get; set; }
        public IStickCommand RightStick { get; set; }

        public List<Type> Commands { get; set; }

        public MainModel()
        {
            Commands = Assembly.GetEntryAssembly().GetTypes().Where(t => t.IsAssignableFrom(typeof(ICommand)) && t.Name != nameof(ICommand)).ToList();
            Load();
        }
        private void Load()
        {
            //todo init
        }
    }
}
