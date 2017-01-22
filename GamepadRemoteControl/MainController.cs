using MaximStartsev.GamepadRemoteControl.Commands;
using MaximStartsev.GamepadRemoteControl.MVC.SetCommand;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl
{
    internal sealed class MainController
    {
        private const string ConfigFilename = "config.xml";

        private MainView _mainView;
        private MainModel _mainModel;
        public MainController()
        {
            _mainView = new MainView();

            if (File.Exists(ConfigFilename))
            {
                var serializer = new XmlSerializer(typeof(MainModel));
                using (var config = File.OpenRead(ConfigFilename))
                {
                    _mainModel = (MainModel)serializer.Deserialize(config);
                }
            }
            else
            {
                _mainModel = new MainModel();
            }
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
                var properies = _mainModel.GetType().GetProperties().Where(p => p.PropertyType.IsAssignableFrom(typeof(ICommand)));
                _mainView.ShowConfig(properies.ToDictionary(p => p.Name, p => {
                    var value = (ICommand)p.GetValue(_mainModel);
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
            new SetCommandController(_mainModel.GetType().GetProperties().Where(p=>p.PropertyType.IsAssignableFrom(typeof(ICommand))),
                _mainModel.Commands,
                (property, commandType) => property.SetValue(_mainModel, Activator.CreateInstance(commandType)))
                    .Run(commandParameters);
        }

        #endregion
    }
}
