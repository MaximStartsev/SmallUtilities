using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteController;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    /// <summary>
    /// todo необходимо доделать:
    /// 1. при перенесении окна учитывать его координаты относительно окна, на котором он был.
    /// 2. учитывать максимизировано ли окно
    /// 3. учитывать полноэкранные приложения
    /// </summary>
    [Alias(Title = "movetonextmonitor")]
    public class MoveToNextMonitorCommand : ButtonCommand
    {
        private Action _action;
        [XmlIgnore]
        public override Action Action
        {
            get
            {
                if (_action == null)
                {
                    _action = new Action(() =>
                    {
                        //Текущий активный процесс
                        var foregroundProcess = InteropHelper.GetActiveProcess();
                        //Следующий экран
                        var screen = GetNextScreen(foregroundProcess);
                        InteropHelper.MoveWindow(foregroundProcess, screen);
                    });
                }
                return _action;
            }
        }
        [XmlIgnore]
        public override string Title
        {
            get
            {
                return "Move foreground window to next monitor";
            }
        }

        private Screen GetNextScreen(Process process)
        {
            var pos = InteropHelper.GetWindowPosition(process.MainWindowHandle);
            var sortedScreens = Screen.AllScreens.OrderBy(s => s.WorkingArea.X);
            var currentScreen = sortedScreens.LastOrDefault(s => s.WorkingArea.Left <= pos.X);
            //Если окно максимизировано, будет екцепшн
            return currentScreen == Screen.AllScreens.Last() ? Screen.AllScreens.First() : Screen.AllScreens.SkipWhile(s => s != currentScreen).Skip(1).First();
        }
    }
}