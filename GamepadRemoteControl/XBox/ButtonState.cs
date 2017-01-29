using MaximStartsev.GamepadRemoteControl.XBox.Events;
using MaximStartsev.GamePadRemoteControl.XBox;
using MaximStartsev.GamePadRemoteControl.XInput.Constants;
using System;
using System.Diagnostics;

namespace MaximStartsev.GamepadRemoteControl.XBox
{
    class ButtonState
    {
        private XBoxController _controller;
        private ButtonFlags _button;
        private bool _isButtonDown;
        private bool _isHandled;

        public event EventHandler<ControllerButtonDownEventArgs> ButtonDown;
        private void InvokeButtonDown(ButtonFlags button)
        {
            ButtonDown?.Invoke(this, new ControllerButtonDownEventArgs(button));
        }

        public event EventHandler<ControllerButtonUpEventArgs> ButtonUp;
        private void InvokeButtonUp(ButtonFlags button)
        {
            ButtonUp?.Invoke(this, new ControllerButtonUpEventArgs(button));
        }

        public event EventHandler<ControllerButtonUpEventArgs> ButtonClick;
        private void InvokeButtonClick(ButtonFlags button)
        {
            ButtonClick(this, new ControllerButtonUpEventArgs(button));
        }

        public ButtonState(XBoxController controller, ButtonFlags button)
        {
            _button = button;
            _controller = controller;
            controller.StateChanged += Controller_StateChanged;
            controller.ButtonDown += Controller_ButtonDown;
            controller.ButtonUp += Controller_ButtonUp;
        }

        private void Controller_ButtonDown(object sender, ControllerButtonDownEventArgs e)
        {
            _isHandled = e.Handled;
        }

        private void Controller_ButtonUp(object sender, ControllerButtonUpEventArgs e)
        {
            if (_isButtonDown)
            {
                if (!e.Handled)
                {
                    InvokeButtonClick(_button);
                }
                Reset();
            }
        }

        private void Controller_StateChanged(object sender, GamePadRemoteControl.XBox.Events.ControllerStateChangedEventArgs e)
        {
            var isPressed = e.CurrentInputState.Gamepad.IsButtonPressed((int)_button);
            if (!_isButtonDown)//Кнопка не нажата. Стандартное состояние
            {
                if (isPressed)
                {
                    _isButtonDown = true;
                    InvokeButtonDown(_button);
                }
            }
            else //Кнопка в нажатом состоянии
            {
                if (_isHandled)//Обработано
                {
                    Reset();
                    return;
                }
                if (!isPressed)//Кнопка отпущена
                {
                    InvokeButtonUp(_button);
                }
            }
        }
        private void Reset()
        {
            _isButtonDown = false;
            _isHandled = false;
        }

    }
}
