using System;
using System.Collections.Generic;

namespace MaximStartsev.GamepadRemoteControl
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
            Console.WriteLine("save - сохраняет конфигурацию приложения.");
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
        #region set-command
        public void ShowButtons(IEnumerable<string> buttons)
        {
            var counter = 0;
            Console.WriteLine("Введите номер или имя кнопки из списка:");
            foreach (var button in buttons)
            {
                Console.WriteLine(String.Format("{0}. {1}", ++counter, button));
            }
        }
        public void ShowCommands(IEnumerable<string> commands)
        {
            var counter = 0;
            Console.WriteLine("Введите номер или имя команды из списка:");
            foreach (var command in commands)
            {
                Console.WriteLine(String.Format("{0}. {1}", ++counter, command));
            }
        }
        public void SetCommandComplete()
        {
            Console.WriteLine("Готово");
        }
        #endregion
    }
}
