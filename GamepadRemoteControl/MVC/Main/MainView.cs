using System;
using System.Collections.Generic;

namespace MaximStartsev.GamepadRemoteControl.MVC.Main
{
    internal sealed class MainView
    {
        public void UnknownCommand()
        {
            Console.WriteLine("Неизвестная команда. Чтобы открыть список доступных команд введите 'help'.");
        }
        public void ShowHelp()
        {
            Console.WriteLine("Список доступных комманд:");
            Console.WriteLine("help - помощь.");
            Console.WriteLine("show-config - отображение на экране текущей конфигурации.");
            Console.WriteLine("set-command - устанавливает значение для параметра.");
            Console.WriteLine("stop или pause - приостановка работы приложения.");
            Console.WriteLine("start - продолжение работы приложения.");
        }
        public void ShowConfig(IDictionary<string, string> commands)
        {
            Console.WriteLine("Текущая конфигурация:");
            foreach (var command in commands)
            {
                Console.WriteLine(String.Format("{0}: {1}", command.Key, String.IsNullOrEmpty(command.Value)? "Не задано" : command.Value));
            }
        }
        public void ShowError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        public void ShowWelcome()
        {
            Console.WriteLine("Введите команду");
        }
    }
}
