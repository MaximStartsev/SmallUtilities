using MaximStartsev.GamepadRemoteControl.Meta;
using MaximStartsev.GamePadRemoteControl.XInput;
using MaximStartsev.GamePadRemoteController;
using System;
using System.Xml.Serialization;

namespace MaximStartsev.GamepadRemoteControl.Commands
{
    [Alias(Title = "scroll")]
    public class ScrollStickCommand : StickCommand
    {
        private const Int32 Сoefficient = 1000;

        [XmlIgnore]
        public override string Title { get { return "Mouse scroll"; } }

        private Action<Point> _action;
        [XmlIgnore]
        public override Action<Point> Action
        {
            get
            {
                if (_action == null)
                {
                    _action = new Action<Point>(point =>
                    {
                        InteropHelper.MouseScroll(point.Y / Сoefficient);
                    });
                }
                return _action;
            }
        }

    }
}
