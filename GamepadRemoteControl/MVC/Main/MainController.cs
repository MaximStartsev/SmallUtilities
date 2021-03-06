﻿using MaximStartsev.GamepadRemoteControl.Commands;
using MaximStartsev.GamepadRemoteControl.Exceptions;
using MaximStartsev.GamepadRemoteControl.MVC.SetCommand;
using MaximStartsev.GamePadRemoteControl.XBox;
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
            while (true)
            {
                try
                {
                    Console.Clear();
                    _mainView.ShowWelcome();
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
                        case "pause":
                        case "stop":
                            Pause();
                            break;
                        case "start":
                            Start();
                            break;
                        default:
                            _mainView.UnknownCommand();
                            break;
                    }
                }
                catch (BreakException) { }
            }
        }
        private void Pause()
        {
            XBoxController.StopPolling();
        }
        private void Start()
        {
            XBoxController.StartPolling();
        }
        private void ShowConfig()
        {
            try
            {
                var properies = MainModel.GetType().GetProperties().Where(p => typeof(Command).IsAssignableFrom(p.PropertyType));
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
            new SetCommandController(MainModel.GetType().GetProperties().Where(p => typeof(Command).IsAssignableFrom(p.PropertyType)),
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
