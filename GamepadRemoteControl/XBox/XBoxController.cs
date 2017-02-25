//***********************************************
// based on a https://www.codeproject.com/articles/492473/using-xinput-to-access-an-xbox-controller-in-m
//**********************************************
using MaximStartsev.GamepadRemoteControl.XBox.Events;
using MaximStartsev.GamePadRemoteControl.XBox.Events;
using MaximStartsev.GamePadRemoteControl.XInput;
using MaximStartsev.GamePadRemoteControl.XInput.Constants;
using System;
using System.Threading;
using System.Linq;
using System.Collections.Generic;
using MaximStartsev.GamepadRemoteControl.XBox;
using System.Diagnostics;

namespace MaximStartsev.GamePadRemoteControl.XBox
{
    //todo Переписать класс, чтобы были события нажатия кнопок и пауза после нажатия

    public sealed class XBoxController
    {
        int _playerIndex;
        static bool keepRunning;
        static int updateFrequency;
        static int waitTime;
        static bool isRunning;
        static object SyncLock;
        static Thread pollingThread;

        bool _stopMotorTimerActive;
        DateTime _stopMotorTime;
        BatteryInformation _batteryInformationGamepad;
        BatteryInformation _batterInformationHeadset;

        State gamepadStatePrev = new State();
        State gamepadStateCurrent = new State();

        public static int UpdateFrequency
        {
            get { return updateFrequency; }
            set
            {
                updateFrequency = value;
                waitTime = 1000 / updateFrequency;
            }
        }

        public BatteryInformation BatteryInformationGamepad
        {
            get { return _batteryInformationGamepad; }
            internal set { _batteryInformationGamepad = value; }
        }

        public BatteryInformation BatteryInformationHeadset
        {
            get { return _batterInformationHeadset; }
            internal set { _batterInformationHeadset = value; }
        }

        public const int MAX_CONTROLLER_COUNT = 4;
        public const int FIRST_CONTROLLER_INDEX = 0;
        public const int LAST_CONTROLLER_INDEX = MAX_CONTROLLER_COUNT - 1;

        static XBoxController[] Controllers;
        #region events
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
            ButtonClick?.Invoke(this, new ControllerButtonUpEventArgs(button));
        }

        public event EventHandler<StickMotionEventArgs> LeftStickMotion;
        private void InvokeLeftStickMotion(Point position)
        {
            LeftStickMotion?.Invoke(this, new StickMotionEventArgs(position));
        }
        public event EventHandler<StickMotionEventArgs> RightStickMotion;
        private void InvokeRightStickMotion(Point position)
        {
            RightStickMotion?.Invoke(this, new StickMotionEventArgs(position));
        }
        #endregion
        static XBoxController()
        {
            Controllers = new XBoxController[MAX_CONTROLLER_COUNT];
            SyncLock = new object();
            for (int i = FIRST_CONTROLLER_INDEX; i <= LAST_CONTROLLER_INDEX; ++i)
            {
                Controllers[i] = new XBoxController(i);
            }
            UpdateFrequency = 25;//Чем больше это число, тем меньше обновлений состояния геймпада, по-умолчанию, 25
        }

        public event EventHandler<ControllerStateChangedEventArgs> StateChanged = null;

        public static XBoxController RetrieveController(int index)
        {
            return Controllers[index];
        }

        private XBoxController(int playerIndex)
        {
            _playerIndex = playerIndex;
            gamepadStatePrev.Copy(gamepadStateCurrent);

            var buttons = (IEnumerable<ButtonFlags>)Enum.GetValues(typeof(ButtonFlags));
            var buttonStates = buttons.Select(b => new ButtonState(this, b));
            foreach (var state in buttonStates)
            {
                state.ButtonDown += State_ButtonDown;
                state.ButtonUp += State_ButtonUp;
                state.ButtonClick += State_ButtonClick;
            }
            (new StickState(this, StickState.StickType.Left)).StickMotion += (s, e) =>
              {
                  InvokeLeftStickMotion(e.Position);
              };
            (new StickState(this, StickState.StickType.Right)).StickMotion+=(s,e)=>
            {
                InvokeRightStickMotion(e.Position);
            };
        }

        private void State_ButtonClick(object sender, ControllerButtonUpEventArgs e)
        {
            InvokeButtonClick(e.Button);
        }

        private void State_ButtonUp(object sender, ControllerButtonUpEventArgs e)
        {
            InvokeButtonUp(e.Button);
        }

        private void State_ButtonDown(object sender, ControllerButtonDownEventArgs e)
        {
            InvokeButtonDown(e.Button);
        }

        public void UpdateBatteryState()
        {
            BatteryInformation headset = new BatteryInformation(),
            gamepad = new BatteryInformation();

            XInput.XInput.XInputGetBatteryInformation(_playerIndex, (byte)BatteryDeviceType.BATTERY_DEVTYPE_GAMEPAD, ref gamepad);
            XInput.XInput.XInputGetBatteryInformation(_playerIndex, (byte)BatteryDeviceType.BATTERY_DEVTYPE_HEADSET, ref headset);

            BatteryInformationHeadset = headset;
            BatteryInformationGamepad = gamepad;
        }

        private void OnStateChanged()
        {
            StateChanged?.Invoke(this, new ControllerStateChangedEventArgs() { CurrentInputState = gamepadStateCurrent, PreviousInputState = gamepadStatePrev });
        }

        public Capabilities GetCapabilities()
        {
            var capabilities = new Capabilities();
            XInput.XInput.XInputGetCapabilities(_playerIndex, Constants.XINPUT_FLAG_GAMEPAD, ref capabilities);
            return capabilities;
        }


        #region Digital Button States
        //public bool IsDPadUpPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.DpadUp); }
        //}

        //public bool IsDPadDownPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.DpadDown); }
        //}

        //public bool IsDPadLeftPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.DPadLeft); }
        //}

        //public bool IsDPadRightPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.DPadRight); }
        //}

        //public bool IsAPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.A); }
        //}

        //public bool IsBPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.B); }
        //}

        //public bool IsXPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.X); }
        //}

        //public bool IsYPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.Y); }
        //}
        //public bool IsBackPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.Back); }
        //}


        //public bool IsStartPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.Start); }
        //}


        //public bool IsLeftShoulderPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.LeftShoulder); }
        //}


        //public bool IsRightShoulderPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.RightShoulder); }
        //}

        //public bool IsLeftStickPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.LeftThumb); }
        //}

        //public bool IsRightStickPressed
        //{
        //    get { return gamepadStateCurrent.Gamepad.IsButtonPressed((int)ButtonFlags.RightThumb); }
        //}
        #endregion

        #region Analogue Input States
        public int LeftTrigger
        {
            get { return (int)gamepadStateCurrent.Gamepad.bLeftTrigger; }
        }

        public int RightTrigger
        {
            get { return (int)gamepadStateCurrent.Gamepad.bRightTrigger; }
        }

        public Point LeftThumbStick
        {
            get
            {
                Point p = new Point()
                {
                    X = gamepadStateCurrent.Gamepad.sThumbLX,
                    Y = gamepadStateCurrent.Gamepad.sThumbLY
                };
                return p;
            }
        }

        public Point RightThumbStick
        {
            get
            {
                Point p = new Point()
                {
                    X = gamepadStateCurrent.Gamepad.sThumbRX,
                    Y = gamepadStateCurrent.Gamepad.sThumbRY
                };
                return p;
            }
        }

        #endregion

        bool _isConnected;
        public bool IsConnected
        {
            get { return _isConnected; }
            internal set { _isConnected = value; }
        }

        #region Polling
        public static void StartPolling()
        {
            if (!isRunning)
            {
                lock (SyncLock)
                {
                    if (!isRunning)
                    {
                        pollingThread = new Thread(PollerLoop);
                        pollingThread.Start();
                    }
                }
            }
        }

        public static void StopPolling()
        {
            if (isRunning)
                keepRunning = false;
        }

        static void PollerLoop()
        {
            lock (SyncLock)
            {
                if (isRunning == true)
                    return;
                isRunning = true;
            }
            keepRunning = true;
            while (keepRunning)
            {
                for (int i = FIRST_CONTROLLER_INDEX; i <= LAST_CONTROLLER_INDEX; ++i)
                {
                    Controllers[i].UpdateState();
                }
                Thread.Sleep(updateFrequency);
            }
            lock (SyncLock)
            {
                isRunning = false;
            }
        }

        public void UpdateState()
        {
            var X = new Capabilities();
            int result = XInput.XInput.XInputGetState(_playerIndex, ref gamepadStateCurrent);
            IsConnected = (result == 0);

            UpdateBatteryState();
            if (gamepadStateCurrent.PacketNumber != gamepadStatePrev.PacketNumber)
            {
                OnStateChanged();
            }
            gamepadStatePrev.Copy(gamepadStateCurrent);

            if (_stopMotorTimerActive && (DateTime.Now >= _stopMotorTime))
            {
                var stopStrength = new Vibration() { LeftMotorSpeed = 0, RightMotorSpeed = 0 };
                XInput.XInput.XInputSetState(_playerIndex, ref stopStrength);
            }
        }
        #endregion

        #region Motor Functions
        public void Vibrate(double leftMotor, double rightMotor)
        {
            Vibrate(leftMotor, rightMotor, TimeSpan.MinValue);
        }

        public void Vibrate(double leftMotor, double rightMotor, TimeSpan length)
        {
            leftMotor = Math.Max(0d, Math.Min(1d, leftMotor));
            rightMotor = Math.Max(0d, Math.Min(1d, rightMotor));

            Vibration vibration = new Vibration() { LeftMotorSpeed = (ushort)(65535d * leftMotor), RightMotorSpeed = (ushort)(65535d * rightMotor) };
            Vibrate(vibration, length);
        }

        public void Vibrate(Vibration strength)
        {
            _stopMotorTimerActive = false;
            XInput.XInput.XInputSetState(_playerIndex, ref strength);
        }

        public void Vibrate(Vibration strength, TimeSpan length)
        {
            XInput.XInput.XInputSetState(_playerIndex, ref strength);
            if (length != TimeSpan.MinValue)
            {
                _stopMotorTime = DateTime.Now.Add(length);
                _stopMotorTimerActive = true;
            }
        }
        #endregion

        public override string ToString()
        {
            return _playerIndex.ToString();
        }

    }
}
