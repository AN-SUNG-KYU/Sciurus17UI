using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using Sciurus17.XBOXInput;
using TUSWHILLCR;
using WHILL_D435i;
using SharpDX.XInput;
using Sciurus17.TcpIp;
using D435RSDepth;
using D435Data;
using D435XBarrier;

namespace CR.WHILL.ControlSystem
{
    public class CRControlSystem　: ICRControlSystem
    {
        public string whill_portname;

        public static string time;

        public static System.IO.StreamWriter writer1, writer2;

        public static bool end = true;

        public sbyte[] whill_input { get; set; }

        public void SetPort(string portname)
        {
            whill_portname = portname;
        }
        public void Run()
        {
            ///コントローラーの入力を受け取る配列
            whill_input = new sbyte[2] { 0, 0 };

            /*rsDepth = MyData.rsDepth;*/
            MyData.rsDepth.Start();
            MyData.rsDepth.PutTask();
            MyData.rsDepth.TakeTask();
            XBarrier barrier = new XBarrier(MyData.rsDepth);
            writer1 = new System.IO.StreamWriter("Sys1_.txt", false);
            writer2 = new System.IO.StreamWriter("Sys2_.txt", false);

            TUSWHILLCR.TUSWHILLCR whill = new TUSWHILLCR.TUSWHILLCR();
            whill.initialize(whill_portname, 20);
            Thread.Sleep(1000);
            whill.printmessage(200);
            whill.startcontrol(20);

            Task.Run(() =>
            {
                Console.ReadKey();
                end = false;

            });

            do
            {

                /*xpadinput = xpad.GetInput();*/
                //CBF.J_F = whill.joystick_forward;
                //CBF.J_T = whill.joystick_turn;

                //CBF.CBF_0();
                //CBF.CBF_F();
                //CBF.CBF_T();

                //whill.forward = CBF.A_F;
                //whill.turn = CBF.A_T;

                /*whill.forward = (sbyte)whill_padinput[0];*/
                whill.forward = (sbyte)(Math.Floor(whill_input[0] + barrier.Barrieru(whill_input[0])));
                whill.turn = (sbyte)whill_input[1];
                

                //whill.forward = whill.joystick_forward;
                //whill.turn = whill.joystick_turn;
                //time = Convert.ToString(realsense.sw.ElapsedMilliseconds);
                //Console.WriteLine(time + "," + CBF.uh_f + "," + CBF.ua_f + "," + CBF.u_f + "," + realsense.e + "," + CBF.R_edge + "," + realsense.e2);
                //writer1.WriteLine(time + "," + CBF.uh_f + "," + CBF.ua_f + "," + CBF.u_f + "," + realsense.e + "," + CBF.R_edge + "," + realsense.e2);
                /*Console.WriteLine(whill.forward + "," + whill.turn);*/
                
                if (whill.forward > 30) whill.forward = 30;
                if (whill.forward < -50) whill.forward = -50;
                Thread.Sleep(10);
                /*}
                else
                {
                    whill.forward = (sbyte)0.0;
                    whill.turn = (sbyte)0.0;
                }*/


                /*if ((padstate.Buttons() & GamepadButtonFlags.Back) != 0)
                {
                    //Console.WriteLine("a");

                    break;
                }*/
            }
            while (true);
            {
                Console.WriteLine("CRWHILL:End!");
                realsense.end2 = false;
            }
        }

        
        /*private sbyte Assist()
        {
            whill.assist_forward = barrier.Barrieru(forwardBuffer);
            *//*if (Math.Abs(assistu) > 5)
            {
                xvibration.RightMotorSpeed = (ushort)(Math.Abs(assistu) * 600);
                xvibration.LeftMotorSpeed = (ushort)(Math.Abs(assistu) * 600);
            }
            else
            {
                xvibration.RightMotorSpeed = (ushort)(0);
                xvibration.LeftMotorSpeed = (ushort)(0);
            }
            xinput.SetVibration(xvibration);*//*
            return (sbyte)(Math.Floor(forwardBuffer + assistu));
        }*/

    }
}
