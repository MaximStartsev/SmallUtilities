using MaximStartsev.GamepadRemoteControl.MVC.Main;
using MaximStartsev.GamePadRemoteControl.XBox;
using MaximStartsev.GamePadRemoteControl.XBox.Events;
using System;
using System.Diagnostics;

namespace MaximStartsev.GamepadRemoteControl
{
    internal sealed class Program
    {
        private static MainController _mainController;
        static void Main(string[] args)
        {
            try
            {
                var controller = XBoxController.RetrieveController(0);
                //controller.StateChanged += Controller_StateChanged;
                controller.ButtonClick += Controller_ButtonClick;
                XBoxController.StartPolling();
                _mainController = new MainController();
                _mainController.Run();
                XBoxController.StopPolling();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.ToString());
            }
        }

        private static void Controller_ButtonClick(object sender, XBox.Events.ControllerButtonUpEventArgs e)
        {
            Debug.WriteLine("Click: "+e.Button);
            switch (e.Button)
            {
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.DpadUp:
                    if(_mainController.MainModel.ButtonDPadUp != null) _mainController.MainModel.ButtonDPadUp.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.DpadDown:
                    if(_mainController.MainModel.ButtonDPadBottom != null) _mainController.MainModel.ButtonDPadBottom.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.DPadLeft:
                    if(_mainController.MainModel.ButtonDPadLeft != null) _mainController.MainModel.ButtonDPadLeft.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.DPadRight:
                    if(_mainController.MainModel.ButtonDPadRight != null) _mainController.MainModel.ButtonDPadRight.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.Start:
                    if (_mainController.MainModel.ButtonStart != null) _mainController.MainModel.ButtonStart.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.Select:
                    if (_mainController.MainModel.ButtonSelect != null) _mainController.MainModel.ButtonSelect.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.LeftThumb:
                    if (_mainController.MainModel.LeftButtonStick != null) _mainController.MainModel.LeftButtonStick.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.RightThumb:
                    if (_mainController.MainModel.RightButtonStick != null) _mainController.MainModel.RightButtonStick.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.LeftShoulder:
                    if (_mainController.MainModel.ButtonLeftTrigger != null) _mainController.MainModel.ButtonLeftTrigger.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.RightShoulder:
                    if (_mainController.MainModel.ButtonRightTrigger != null) _mainController.MainModel.ButtonRightTrigger.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.A:
                    if (_mainController.MainModel.ButtonA != null) _mainController.MainModel.ButtonA.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.B:
                    if (_mainController.MainModel.ButtonB != null) _mainController.MainModel.ButtonB.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.X:
                    if (_mainController.MainModel.ButtonX != null) _mainController.MainModel.ButtonX.Action();
                    break;
                case GamePadRemoteControl.XInput.Constants.ButtonFlags.Y:
                    if (_mainController.MainModel.ButtonY != null) _mainController.MainModel.ButtonY.Action();
                    break;
            }
        }

        //private static void Controller_StateChanged(object sender, ControllerStateChangedEventArgs e)
        //{
        //    var gamepad = sender as XBoxController;
        //    if (gamepad.IsAPressed && _mainController.MainModel.ButtonA != null)
        //    {
        //        _mainController.MainModel.ButtonA.Action();
        //    }
        //    if (gamepad.IsBPressed && _mainController.MainModel.ButtonB != null)
        //    {
        //        _mainController.MainModel.ButtonB.Action();
        //    }
        //    if(gamepad.IsDPadDownPressed && _mainController.MainModel.ButtonDPadBottom != null)
        //    {
        //        _mainController.MainModel.ButtonDPadBottom.Action();
        //    }
        //    if(gamepad.IsDPadLeftPressed && _mainController.MainModel.ButtonDPadLeft != null)
        //    {
        //        _mainController.MainModel.ButtonDPadLeft.Action();
        //    }
        //    if(gamepad.IsDPadRightPressed && _mainController.MainModel.ButtonDPadRight != null)
        //    {
        //        _mainController.MainModel.ButtonDPadRight.Action();
        //    }
        //    if(gamepad.IsDPadUpPressed && _mainController.MainModel.ButtonDPadUp != null)
        //    {
        //        _mainController.MainModel.ButtonDPadUp.Action();
        //    }
        //    if (gamepad.IsXPressed && _mainController.MainModel.ButtonX != null)
        //    {
        //        _mainController.MainModel.ButtonX.Action();
        //    }
        //    if (gamepad.IsYPressed && _mainController.MainModel.ButtonY != null)
        //    {
        //        _mainController.MainModel.ButtonY.Action();
        //    }
        //    //todo Сделать привязки, чтобы не писать каждую команду отдельно
        //}
    }
}
