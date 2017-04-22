using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteController;
using System;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    //todo Доделать аналогично перемещению на следующий экран
    [Alias(Title = "movetoprevmonitor")]
    public sealed class MoveToPrevMonitorCommand:ButtonCommand
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
                        var screen = GetPrevScreen(foregroundProcess);
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

        private Screen GetPrevScreen(Process process)
        {
            var pos = InteropHelper.GetWindowPosition(process.MainWindowHandle);
            var sortedScreens = Screen.AllScreens.OrderByDescending(s => s.WorkingArea.X);
            var currentScreen = sortedScreens.LastOrDefault(s => s.WorkingArea.Left >= pos.X);
            if (currentScreen == null) return Screen.AllScreens.Last();
            
            return currentScreen == Screen.AllScreens.Last() ? Screen.AllScreens.First() : Screen.AllScreens.SkipWhile(s => s != currentScreen).Skip(1).First();
        }
    }
}
