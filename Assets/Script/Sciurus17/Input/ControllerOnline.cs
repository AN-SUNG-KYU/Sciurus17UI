using Sciurus17.TcpIp;
using SharpDX.XInput;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sciurus17.Input
{
    public class ControllerOnline : IController
    {
        public double RightThumbX { get; set; } = 0.0;
        public double RightThumbY { get; set; } = 0.0;
        public double LeftThumbX { get; set; } = 0.0;
        public double LeftThumbY { get; set; } = 0.0;
        public double RightTrigger { get; set; } = 0.0;
        public double LeftTrigger { get; set; } = 0.0;
        public bool DPadUp { get; set; } = false;
        public bool DPadDown { get; set; } = false;
        public bool DPadRight { get; set; } = false;
        public bool DPadLeft { get; set; } = false;
        public bool RightShoulder { get; set; } = false;
        public bool LeftShoulder { get; set; } = false;
        public bool ButtonX { get; set; } = false;
        public bool ButtonY { get; set; } = false;
        public bool ButtonA { get; set; } = false;
        public bool ButtonB { get; set; } = false;
        public bool ButtonStart { get; set; } = false;
        public bool ButtonBack { get; set; } = false;

        public bool Connect { get; set; } = true;

        Receive_TcpIP Tcpip;


        public ControllerOnline(Receive_TcpIP op)
        {
            Tcpip = op;
        }

        public void Update()
        {
            if ((Tcpip.RightThumbX > 2000) || (Tcpip.RightThumbX < -2000)) RightThumbX = Tcpip.RightThumbX / 32767.0;
            else RightThumbX = 0.0;

            if ((Tcpip.RightThumbY > 2000) || (Tcpip.RightThumbY < -2000)) RightThumbY = Tcpip.RightThumbY / 32767.0;
            else RightThumbY = 0.0;

            if ((Tcpip.LeftThumbX > 2000) || (Tcpip.LeftThumbX < -2000)) LeftThumbX = Tcpip.LeftThumbX / 32767.0;
            else LeftThumbX = 0.0;

            if ((Tcpip.LeftThumbY > 2000) || (Tcpip.LeftThumbY < -2000)) LeftThumbY = Tcpip.LeftThumbY / 32767.0;
            else LeftThumbY = 0.0;

            if (Tcpip.RightTrigger > 0) RightTrigger = Tcpip.RightTrigger / 255.0;
            else RightTrigger = 0.0;

            if (Tcpip.LeftTrigger > 0) LeftTrigger = Tcpip.LeftTrigger / 255.0;
            else LeftTrigger = 0.0;

            DPadUp = Tcpip.Buttons.HasFlag(GamepadButtonFlags.DPadUp);
            DPadDown = Tcpip.Buttons.HasFlag(GamepadButtonFlags.DPadDown);
            DPadRight = Tcpip.Buttons.HasFlag(GamepadButtonFlags.DPadRight);
            DPadLeft = Tcpip.Buttons.HasFlag(GamepadButtonFlags.DPadLeft);
            RightShoulder = Tcpip.Buttons.HasFlag(GamepadButtonFlags.RightShoulder);
            LeftShoulder = Tcpip.Buttons.HasFlag(GamepadButtonFlags.LeftShoulder);
            ButtonX = Tcpip.Buttons.HasFlag(GamepadButtonFlags.X);
            ButtonY = Tcpip.Buttons.HasFlag(GamepadButtonFlags.Y);
            ButtonA = Tcpip.Buttons.HasFlag(GamepadButtonFlags.A);
            ButtonB = Tcpip.Buttons.HasFlag(GamepadButtonFlags.B);
            ButtonStart = Tcpip.Buttons.HasFlag(GamepadButtonFlags.Start);
            ButtonBack = Tcpip.Buttons.HasFlag(GamepadButtonFlags.Back);
        }

    }
}
