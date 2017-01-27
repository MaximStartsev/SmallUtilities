using MaximStartsev.GamepadRemoteControl.Commands;
using MaximStartsev.GamepadRemoteControl.Meta;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MaximStartsev.GamepadRemoteControl.MVC.SetCommand
{
    //todo параметры команды
    internal sealed class SetCommandController
    {
        private readonly SetCommandModel _model;
        private readonly SetCommandView _view;
        private readonly Action<PropertyInfo, Type, Dictionary<PropertyInfo, string>> _setCommandAction;
        public SetCommandController(IEnumerable<PropertyInfo> buttons, IEnumerable<Type> commands, Action<PropertyInfo, Type, Dictionary<PropertyInfo, string>> setCommandAction)
        {
            _model = new SetCommandModel(buttons, commands);
            _view = new SetCommandView();
            _setCommandAction = setCommandAction;
        }
        public void Run(IEnumerable<string> arguments)
        {
            if (arguments.Count() >= 1)
            {
                var buttonName = arguments.First().ToLower();
                var buttonProp = _model.Buttons.FirstOrDefault(prop => prop.Name.ToLower() == buttonName);
                if (buttonProp == null)
                {
                    _view.ShowError(new Exception(String.Format("Не удалось найти кнопку с именем '{0}'.", arguments.First())));
                    buttonProp = GetButton();
                }
                if (arguments.Count() >= 2)
                {
                    var commandName = arguments.ElementAt(1).ToLower();
                    var commandType = _model.Commands.FirstOrDefault(t => SelectCommand(t, commandName));
                    if (commandType == null)
                    {
                        _view.ShowError(new Exception(String.Format("Не удалось найти команду '{0}'", arguments.ElementAt(1))));
                        commandType = GetCommand();
                    }
                    //todo сделать так же парсинг параметров
                    var parameters = GetCommandParameters(commandType);
                    SetCommand(buttonProp, commandType, parameters);
                }
                else
                {
                    var command = GetCommand();
                    var parameters = GetCommandParameters(command);
                    SetCommand(buttonProp, command, parameters);
                }
            }
            else
            {
                var button = GetButton();
                var command = GetCommand();
                var parameters = GetCommandParameters(command);
                SetCommand(button, command, parameters);
            }
        }

        private PropertyInfo GetButton()
        {
            var buttons = _model.Buttons.Where(t => typeof(Command).IsAssignableFrom(t.PropertyType)).Select(b => b.Name);
            string propertyName = null;
            do
            {
                _view.ShowButtons(buttons);
                var button = Console.ReadLine().ToLower();
                int buttonNumber;
                if (Int32.TryParse(button, out buttonNumber))
                {
                    buttonNumber--;
                    if (buttonNumber >= 0 && buttonNumber < buttons.Count())
                    {
                        propertyName = buttons.ElementAt(buttonNumber);
                    }
                }
                else
                {
                    propertyName = buttons.FirstOrDefault(b => b.ToLower() == button);
                }
                if (String.IsNullOrEmpty(propertyName))
                {
                    _view.ShowError(new Exception("Введено неверное имя кнопки. Повторите попытку."));
                }
            }
            while (String.IsNullOrEmpty(propertyName));
            return _model.Buttons.First(p => p.Name.ToLower() == propertyName.ToLower());
        }

        private Type GetCommand()
        {
            var commands = _model.Commands.Select(c =>
            {
                var attribute = c.CustomAttributes.FirstOrDefault();
                if (attribute != null)
                {
                    return attribute.NamedArguments[0].TypedValue.Value as String;
                }
                return c.Name;
            });
            string commandName = null;
            do
            {
                _view.ShowCommands(commands);
                var command = Console.ReadLine().ToLower();
                int commandNumber;
                if (Int32.TryParse(command, out commandNumber))
                {
                    commandNumber--;
                    if (commandNumber >= 0 && commandNumber < commands.Count())
                    {
                        commandName = commands.ElementAt(commandNumber);
                    }
                }
                else
                {
                    commandName = commands.FirstOrDefault(c => c.ToLower() == command);
                }
                if (String.IsNullOrEmpty(commandName))
                {
                    _view.ShowError(new Exception("Введено неверное имя команды. Повторите попытку."));
                }
            }
            while (String.IsNullOrEmpty(commandName));
            return _model.Commands.First(t => SelectCommand(t, commandName));
        }
        private bool SelectCommand(Type commandType, string commandName)
        {
            if (commandType.Name == commandName) return true;
            var attribute = commandType.CustomAttributes.FirstOrDefault();
            if (attribute != null)
            {
                var value = attribute.NamedArguments[0].TypedValue.Value as String;
                if (value == commandName) return true;
            }
            return false;
        }
        private Dictionary<PropertyInfo, string> GetCommandParameters(Type commandType)
        {
            var props = commandType.GetProperties();
            var parameters = commandType.GetProperties().Where(p=>p.CustomAttributes.Any() && p.CustomAttributes.First().AttributeType == typeof(CommandParameterAttribute));
            if (!parameters.Any()) return new Dictionary<PropertyInfo, string>();
            return parameters.ToDictionary(p => p, p => GetCommandParameter(p));
        }
        private string GetCommandParameter(PropertyInfo property)
        {
            _view.ShowCommandParameter(property.Name);
            return Console.ReadLine();
        }

        private void SetCommand(PropertyInfo property, Type commandType, Dictionary<PropertyInfo, string> parameters)
        {
            _setCommandAction(property, commandType, parameters);
            _view.SetCommandComplete();
        }
    }

}
