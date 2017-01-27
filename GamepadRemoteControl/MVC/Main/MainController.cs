using MaximStartsev.GamepadRemoteControl.Commands;
using MaximStartsev.GamepadRemoteControl.MVC.SetCommand;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MaximStartsev.GamepadRemoteControl.MVC.Main
{
    internal sealed class MainController
    {

        private MainView _mainView;
        public MainModel MainModel;
        public MainController()
        {
            _mainView = new MainView();
            MainModel = MainModelSerializer.Load();
        }

        public void Run()
        {
            DoCommand();
        }

        private void DoCommand()
        {
            var line = Console.ReadLine();
            var words = line.Split(new[] { ' ' });
            switch (words[0].ToLowerInvariant())
            {
                case "help":
                case "?":
                case "h":
                    _mainView.ShowHelp();
                    break;
                case "show-config":
                case "showconfig":
                    ShowConfig();
                    break;
                case "set-command":
                case "setcommand":
                    SetCommand(words.Skip(1));
                    break;
                default:
                    _mainView.UnknownCommand();
                    break;
            }
            DoCommand();
        }
        private void ShowConfig()
        {
            try
            {
                var properies = MainModel.GetType().GetProperties().Where(p => p.PropertyType.IsAssignableFrom(typeof(Command)));
                _mainView.ShowConfig(properies.ToDictionary(p => p.Name, p => {
                    var value = (Command)p.GetValue(MainModel);
                    return value == null ? String.Empty : value.GetType().Name;
                }));
            }
            catch (Exception ex)
            {
                _mainView.ShowError(ex);
            }
        }
        #region set command
        private void SetCommand(IEnumerable<string> commandParameters)
        {
            new SetCommandController(MainModel.GetType().GetProperties().Where(p => p.PropertyType.IsAssignableFrom(typeof(Command))),
                MainModel.Commands,
                (property, commandType, parameters) =>
                {
                    var command = Activator.CreateInstance(commandType);
                    foreach (var parameter in parameters)
                    {
                        parameter.Key.SetValue(command, parameter.Value);
                    }
                    property.SetValue(MainModel, command);
                    MainModelSerializer.Save(MainModel);
                }).Run(commandParameters);
        }

        #endregion
    }
}
