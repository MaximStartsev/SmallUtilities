using System;
using System.Collections.Generic;

namespace MaximStartsev.GamepadRemoteControl.MVC.SetCommand
{
    internal sealed class SetCommandView
    {
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
        public void ShowError(Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(ex.ToString());
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
