using MaximStartsev.GamepadRemoteControl.MVC.Main;
using MaximStartsev.GamePadRemoteControl.XBox;
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
                controller.StateChanged += Controller_StateChanged;
                XBoxController.StartPolling();
                _mainController = new MainController();
                _mainController.Run();
                XBoxController.StopPolling();
            }
            catch (System.Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }

        //Не очень красиво, зато просто
        private static void Controller_StateChanged(object sender, ControllerStateChangedEventArgs e)
        {
            var gamepad = sender as XBoxController;
            if (gamepad.IsAPressed && _mainController.MainModel.ButtonA != null)
            {
                _mainController.MainModel.ButtonA.Action();
            }
            if (gamepad.IsBPressed && _mainController.MainModel.ButtonB != null)
            {
                _mainController.MainModel.ButtonB.Action();
            }
            if(gamepad.IsDPadDownPressed && _mainController.MainModel.ButtonDPadBottom != null)
            {
                _mainController.MainModel.ButtonDPadBottom.Action();
            }
            if(gamepad.IsDPadLeftPressed && _mainController.MainModel.ButtonDPadLeft != null)
            {
                _mainController.MainModel.ButtonDPadLeft.Action();
            }
            if(gamepad.IsDPadRightPressed && _mainController.MainModel.ButtonDPadRight != null)
            {
                _mainController.MainModel.ButtonDPadRight.Action();
            }
            if(gamepad.IsDPadUpPressed && _mainController.MainModel.ButtonDPadUp != null)
            {
                _mainController.MainModel.ButtonDPadUp.Action();
            }
            if (gamepad.IsXPressed && _mainController.MainModel.ButtonX != null)
            {
                _mainController.MainModel.ButtonX.Action();
            }
            if (gamepad.IsYPressed && _mainController.MainModel.ButtonY != null)
            {
                _mainController.MainModel.ButtonY.Action();
            }
            //todo Сделать привязки, чтобы не писать каждую команду отдельно
        }
    }
}
