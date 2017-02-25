using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteController;
using System;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "mouseclick")]
    public sealed class MouseClickCommand : ButtonCommand
    {
        //todo: Сделать, чтобы можно было задавать параметр с типом кнопки
        //[CommandParameter]
        //public string Button { get; set; }
        private Action _action;
        public override Action Action
        {
            get
            {
                if(_action == null)
                {
                    _action = new Action(()=>
                    {
                        InteropHelper.MouseLeftClick();
                    });
                }
                return _action;
            }
        }

        public override string Title { get { return "Mouse Click"; } }
    }
}
