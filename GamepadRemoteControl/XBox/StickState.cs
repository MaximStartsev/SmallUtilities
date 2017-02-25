using MaximStartsev.GamepadRemoteControl.XBox.Events;
using MaximStartsev.GamePadRemoteControl.XBox;
using MaximStartsev.GamePadRemoteControl.XInput;
using System;
using System.Diagnostics;

namespace MaximStartsev.GamepadRemoteControl.XBox
{

    public class StickState
    {
        private XBoxController _controller;
        private StickType _stickType;
        private Point _previosValue;

        public event EventHandler<StickMotionEventArgs> StickMotion;
        public StickState(XBoxController controller, StickType type)
        {
            _controller = controller;
            _stickType = type;
            _previosValue = new Point(0, 0);
            _controller.StateChanged += _controller_StateChanged;
        }

        private void _controller_StateChanged(object sender, GamePadRemoteControl.XBox.Events.ControllerStateChangedEventArgs e)
        {
            switch (_stickType)
            {
                case StickType.Left:
                    if (!_controller.LeftThumbStick.Equals(_previosValue))
                    {
                        InvokeStickMotion(_controller.LeftThumbStick);
                    }
                    break;
                case StickType.Right:
                    if (!_controller.RightThumbStick.Equals(_previosValue))
                    {
                        InvokeStickMotion(_controller.RightThumbStick);
                    }
                    break;
            }
        }
        private void InvokeStickMotion(Point point)
        {
            StickMotion?.Invoke(this, new StickMotionEventArgs(point));
        }
        public enum StickType
        {
            Left,
            Right
        }
    }
}
