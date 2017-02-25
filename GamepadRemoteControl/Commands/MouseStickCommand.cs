using System;
using MaximStartsev.GamePadRemoteControl.XInput;
using System.Windows.Forms;
using MaximStartsev.GamepadRemoteControl.Meta;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "mouse")]
    public sealed class MouseStickCommand : StickCommand
    {
        private Action<Point> _action;
        public override Action<Point> Action
        {
            get
            {
                if(_action == null)
                {
                    _action = new Action<Point>(point =>
                    {
                       // var cursor = new Cursor(Cursor.Current.Handle);
                        Cursor.Position = new System.Drawing.Point(Cursor.Position.X + Convert.ToInt16(point.X / 1000), Cursor.Position.Y + Convert.ToInt16(point.Y / -1000));//point.Y должно использоваться с противоположным знаком
                    });
                }
                return _action;
            }
        }

        public override string Title { get { return "Mouse motion"; } }
    }
}
