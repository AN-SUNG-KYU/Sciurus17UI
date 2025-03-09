using SharpDX.XInput;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.Input
{
    public class ControllerOff:IController
    {
        public double RightThumbX {  get; set; }
        public double RightThumbY { get; set; }
        public double LeftThumbX { get; set; }
        public double LeftThumbY { get; set; }
        public double RightTrigger { get; set; }
        public double LeftTrigger { get; set;}
        public bool DPadUp { get; set; }
        public bool DPadDown { get; set; }
        public bool DPadRight { get; set; }
        public bool DPadLeft { get; set; }
        public bool RightShoulder { get; set; }
        public bool LeftShoulder { get; set; }
        public bool ButtonX { get; set; }
        public bool ButtonY { get; set; }
        public bool ButtonA { get; set; }
        public  bool ButtonB { get; set; }
        public bool ButtonStart { get; set; }
        public bool ButtonBack { get; set; }

        public bool Connect { get; set; } = true;
 
        private State state;
        private Controller Controller;
        GamepadButtonFlags St;

        public ControllerOff()
        {
            Controller = new Controller(UserIndex.One);
            if (!Controller.IsConnected)
            {
                Console.WriteLine("XBOXのコントローラが接続されていません");
                Connect = false;
            }
            else 
            {
                Console.WriteLine("XBOXのコントローラが接続されました");
            }
        }

        public void Update()
        {
            if (!Controller.IsConnected)
            {
                Console.WriteLine("XBOXのコントローラの接続がきれました");
                Connect = false;
            }
            else
            {
                state = Controller.GetState();
                if ((state.Gamepad.RightThumbX > 2000) || (state.Gamepad.RightThumbX < -2000)) RightThumbX = state.Gamepad.RightThumbX / 32767.0;
                else RightThumbX = 0.0;

                if (state.Gamepad.RightThumbY > 2000) RightThumbY = state.Gamepad.RightThumbY / 32767.0;
                else if (state.Gamepad.RightThumbY < -2000)RightThumbY = state.Gamepad.RightThumbY / 32768.0;
                else RightThumbY = 0.0;

                if ((state.Gamepad.LeftThumbX > 2000) || (state.Gamepad.LeftThumbX < -2000)) LeftThumbX = state.Gamepad.LeftThumbX / 32767.0;
                else LeftThumbX = 0.0;

                if ((state.Gamepad.LeftThumbY > 2000) || (state.Gamepad.LeftThumbY < -2000)) LeftThumbY = state.Gamepad.LeftThumbY / 32767.0;
                else LeftThumbY = 0.0;

                if (state.Gamepad.RightTrigger > 0) RightTrigger = state.Gamepad.RightTrigger / 255.0;
                else RightTrigger = 0.0;

                if (state.Gamepad.LeftTrigger > 0) LeftTrigger = state.Gamepad.LeftTrigger / 255.0;
                else LeftTrigger = 0.0;

                DPadUp = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadUp);
                DPadDown = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadDown);
                DPadRight = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadRight);
                DPadLeft = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.DPadLeft);
                RightShoulder = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.RightShoulder);
                LeftShoulder = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder);
                ButtonX = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.X);
                ButtonY = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Y);
                ButtonA = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.A);
                ButtonB = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.B);
                ButtonStart = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Start);
                ButtonBack = state.Gamepad.Buttons.HasFlag(GamepadButtonFlags.Back);
            }

        }
    }
}
