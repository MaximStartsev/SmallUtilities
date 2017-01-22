using System;
using System.Collections.Generic;
using System.Reflection;

namespace MaximStartsev.GamepadRemoteControl.MVC.SetCommand
{
    internal sealed class SetCommandModel
    {
        public IEnumerable<PropertyInfo> Buttons { get; private set; }
        public IEnumerable<Type> Commands { get; private set; }
        public SetCommandModel(IEnumerable<PropertyInfo> buttons, IEnumerable<Type> commands)
        {
            Buttons = buttons;
            Commands = commands;
        }
    }
}
